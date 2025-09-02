<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="Final11373.Patient.Survey" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patient Survey</title>
    <style>
        .survey-container { width: 500px; margin: 50px auto; border: 1px solid #ccc; border-radius: 10px; padding: 20px; background-color: #f9f9f9; font-family: Arial; }
        h2 { text-align: center; color: #3366ff; }
        .section { margin-bottom: 20px; }
        .section label { font-weight: bold; display: block; margin-bottom: 5px; }
        .rbl-horizontal td { padding-right: 15px; }
        .btn-submit { background-color: #3366ff; color: white; border: none; padding: 10px 25px; border-radius: 6px; cursor: pointer; font-size: 15px; }
        .status-label { color: green; margin-top: 15px; text-align: center; display: block; }
        .textbox-style { width: 100%; height: 50px; padding: 5px; font-size: 14px; border-radius: 5px; border: 1px solid #ccc; }
    </style>
</head>
<body>
    <!-- ✅ All server controls must be inside this form -->
    <form id="form1" runat="server">
        <div class="survey-container">
            <h2>
                <asp:Button ID="btnBack" runat="server" Text="⬅ Back to Home" CssClass="btn-submit" Visible="false" OnClick="btnBack_Click" />
                📋 Daily Health Survey
                </h2>

            <div class="section">
                <label>How do you feel today?</label>
                <asp:RadioButtonList ID="rblMood" runat="server" RepeatDirection="Horizontal" CssClass="rbl-horizontal">
                    <asp:ListItem Text="😊 Good" Value="Good" />
                    <asp:ListItem Text="😐 Okay" Value="Okay" />
                    <asp:ListItem Text="😟 Bad" Value="Bad" />
                </asp:RadioButtonList>
            </div>

            <div class="section">
                <label>Select any symptoms you feel:</label><br />
                <asp:CheckBoxList ID="cbxSymptoms" runat="server" RepeatDirection="Vertical" RepeatLayout="Table">
                    <asp:ListItem Text="Headache" Value="Headache" />
                    <asp:ListItem Text="Nausea" Value="Nausea" />
                    <asp:ListItem Text="Dizziness" Value="Dizziness" />
                    <asp:ListItem Text="Trouble Sleeping" Value="Sleep Trouble" />
                </asp:CheckBoxList>
            </div>

            <div class="section">
                <label>Did you notice any changes in your health today?</label>
                <asp:TextBox ID="txtChanges" runat="server" TextMode="MultiLine" CssClass="textbox-style" />
            </div>

            <div class="section">
                <label>Is there anything that made you uncomfortable or worried today?</label>
                <asp:TextBox ID="txtConcerns" runat="server" TextMode="MultiLine" CssClass="textbox-style" />
            </div>

            <asp:Button ID="btnSubmitSurvey" runat="server" Text="Submit Survey" CssClass="btn-submit" OnClick="btnSubmitSurvey_Click" />

            <!-- ✅ Keep these INSIDE the form -->
            <asp:Label ID="lblSurveyStatus" runat="server" CssClass="status-label" />
            <br />
        </div>
    </form>
</body>
</html>
