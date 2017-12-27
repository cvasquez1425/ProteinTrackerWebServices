<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProteinTracker.aspx.cs" Inherits="ProteinTrackerWebServices.Pages.ProteinTracker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Protein Tracker</title>
    <script type="text/javascript" src="../Scripts/jquery-3.1.1.min.js"></script>
    <script type="text/javascript">


        // calling these services on your page without using the ASP.NET built-in-support.  if you wanna make JQuery AJAX support to make your call, 

        var _users;  // be careful with a real app with the Global Scope in JavaScript.

        //this function will fire whene the document is ready, basically when the page loads up.
        $(function () {
            // add a hook to basically respond to the change event for select-user
            $("#select-user").change(function () {
                UpdateUserData();
            });

            PopulateSelectUsers();
        });

        function PopulateSelectUsers() {
            var selectUser = $("#select-user");
            // make our Web Service call to get the data and then populate this.
            selectUser.empty();         // let's make sure we empty selectUser.
            // users is a JSON object.
            // <option value="0">Joe</option>
            // <option value="1">Bill</option>
            //ProteinTrackerWebServices.ProteinTrackingService.ListUsers(function (users) {     // we pass a callback funtion, so this is going to be happening asynchronously, when it finishes it will do something (callback function)
                PageMethods.ListUsers(function (users) {                                        // it is going to be a JSON object, it will match the C# class user

                _users = users;
                for (var i = 0; i < users.length; i++) {
                    selectUser.append($("<option></option")                                     // append an option for each one of these iterations of this loop.
                        .attr("value", users[i].UserId)
                        .text(users[i].Name));
                }
                //<select name="favorites"> 
                //        <option value="American">American flamingo</option> 
                //        <option value="Greater">Greater flamingo</option>  
                //</select>
                
                UpdateUserData();
            });
        }

        function AddNewUser() {
            var $name = $("#name").val();
            var $goal = $("#goal").val();
            // third parm is a callback function
            //ProteinTrackerWebServices.ProteinTrackingService.AddUser($name, $goal, function () {

            // using the page method on the ProteinTracker.aspx
            //PageMethods.AddUser($name, $goal, function () {            
            //                 PopulateSelectUsers();
            //});

            // AJAX - JQuery version. This is using JQuery's AJAX support. We're just using JQuery to do that like any other AJAX call.
            $.ajax({                                                        // here we pass in a JavaScript object that defines the AJAX call.
                type: "POST",
                url: "proteinTracker.aspx/AddUser",
                data: JSON.stringify({ 'name': $name, 'goal': $goal }),  // {"name":"Bill","goal":"100"} We need our data into JSON object and make it into string.
                contentType: 'application/json; charset=utf-8',           // the content type, very important, get back JSON data instead of the page
                dataType: "json",
                success: PopulateSelectUsers                                // On success 
            });
        }

        function AddProtein() {
            var $amount = $("#amount").val();
            var $userId = $("#select-user").val();
            // using ASP.NET Web Services ProteinTrackerWebServices.ProteinTrackingService. ASMX Web Services
            //ProteinTrackerWebServices.ProteinTrackingService.AddProtein($amount, $userId, function (newTotal) {

            //we will be using Page methods instead of the ASMX Web services. we are going to call those methods in the code-behind file. 
            // so just a handy way if you don't have web services in your project, or you just have some simple AJAX call then all your web methods will be accessible from PageMethods. the name of the method
            PageMethods.AddProtein($amount, $userId, function (newTotal) {
                // update total when switching users
                for (var i = 0; i < _users.length; i++) {
                    if (_users[i].UserId == $userId)
                        _users[i].Total = newTotal;
                }
                //$("#user-total").text(newTotal);
                UpdateUserData();
            });
        }

        function UpdateUserData() {
            var index = $("#select-user")[0].selectedIndex;  // the selectedindex of whatever item is selected, and check to see if the index is less than 0
            if (index < 0)                                   // there's nothing in that dropdown, or we don't have anything selected.
                return;
            $("#user-goal").text(_users[index].Goal);
            $("#user-total").text(_users[index].Total);
        }
    </script>
</head>
<body>
    <!--            we are now using Page Methods. Calling those methods from the page instead of using ASMX Web Services.-->
    <form id="form1" runat="server">

        <!-- we don't even need ScriptManager if we are using AJAX JQuery approach. -->
        <!-- ScriptManager is what this will do, it'll generate some JavaScript code for us to make it easier to call our web services -->
        <!-- EnablePageMethods will allow us to basically call web methods that exist on our page, that is the code-behind ProteinTracker.aspx.cs -->
        <asp:ScriptManager runat="server" EnablePageMethods="true"> 
        </asp:ScriptManager>
                
        <!--<Services>  <asp:ServiceReference Path="~/ProteinTrackingService.asmx" /> </Services>-->

        <h1>Protein Tracker</h1>
        <div>
            <!-- I am just going to use a regular select, we're NOT going to use the ASP.NET control because we don't really need to
                we can just get away with just a regular HTML elemeent because we don't have to actually post back to the server
                this is a little bit lighter. -->
            <label for="select-user">Select a user</label>
            <select id="select-user"></select>
        </div>
        <hr />
        <div>
            <h2>Add new user</h2>
            <br />
            <label for="name">Name</label>
            <input id="name" type="text" /><br />
            <label for="goal">Goal</label>
            <input id="goal" type="text" /><br />
            <input id="add-new-user-button" type="button" onclick="AddNewUser()" value="Add" />
        </div>
        <hr />
        <div>
            <h2>Add Protein</h2>
            <label for="amount">Amount</label>
            <input id="amount" type="text" /><br />
            <input id="add-button" type="button" onclick="AddProtein()" value="Add" />
        </div>
        <div>
            Total: <span id="user-total"></span>
            <br />
            Goal:  <span id="user-goal"></span>
        </div>
    </form>
</body>
</html>
