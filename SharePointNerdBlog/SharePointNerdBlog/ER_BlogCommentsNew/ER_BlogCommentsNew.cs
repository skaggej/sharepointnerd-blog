using System;
using Microsoft.SharePoint;

namespace SharePointNerdBlog.ER_BlogCommentsNew
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ER_BlogCommentsNew : SPItemEventReceiver
    {
       /// <summary>
       /// An item is being added.
       /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        { 
            SPWeb rootWeb = properties.Web.Site.RootWeb;
            try
            {
                string validMessage = properties.AfterProperties["Validation"].ToString();
                if (validMessage == "Colorless Green Ideas Sleep Furiously")
                {
                    base.ItemAdding(properties);
                }
                else
                {
                    //Cancels adding the item
                    properties.Cancel = true;
                    properties.ErrorMessage = "Your comment was not submitted successfully.  Please ensure you've selected the correct validation message and try again.  Thank you.";
                    return;
                }
            }
            catch (Exception exception)
            {
                SPList errorsList = rootWeb.Lists["Errors"];
                SPListItem newError = errorsList.Items.Add();
                newError["Message"] = exception.Message;
                newError["Source"] = exception.Source;
                newError["StackTrace"] = exception.StackTrace;
                newError["InnerException"] = exception.InnerException;
                newError.Update();
                return;  //ensures the error only occurs once
            }
        }
    }
}