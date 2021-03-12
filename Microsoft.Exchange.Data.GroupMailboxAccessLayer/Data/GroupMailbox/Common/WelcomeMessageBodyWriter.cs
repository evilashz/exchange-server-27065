﻿using System;
using System.IO;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x02000043 RID: 67
	internal sealed class WelcomeMessageBodyWriter
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		public static void WriteTemplate(TextWriter writer, WelcomeMessageBodyData data)
		{
			writer.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" /> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <title>Welcome</title> </head> <body style=\"height:100%!important;width:100%!important;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;margin:0;padding:0\"> <style>body,#bodyTable,#bodyCell{height:100%!important;margin:0;padding:0;width:100%!important;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif}table{border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif}img,a img{border:0;outline:0;text-decoration:none}h1,h2,h3,h4,h5,h6{margin:0;padding:0;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif}p{margin:1em 0;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif}div,a,span,p{font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif}.ReadMsgBody{width:100%}.ExternalClass{width:100%}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div{line-height:100%}table,td{mso-table-lspace:0;mso-table-rspace:0}img{-ms-interpolation-mode:bicubic}body,table,td,p,a,li,blockquote{-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%}.textContainer{font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif!important;text-align:left}a{text-decoration:none!important}a:active{text-decoration:none!important}a:hover{text-decoration:none!important}a:visited{text-decoration:none!important}.link{text-decoration:none!important}.mailContainer{width:100%;overflow:hidden}.centerAligned{display:block;margin:0 auto;background-color:#fdfdfd}img{outline:0;text-decoration:none;-ms-interpolation-mode:bicubic}a img{border:0}table,tr,td{padding:0;margin:0;border-spacing:0}.image_fix{display:block}.left-align,td[align=left]{text-align:left}.max-wide-table{max-width:600px;margin:0 auto;padding:0;border:0;mso-table-lspace:0;mso-table-rspace:0}.segeo-font{font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif}#contianerCell{color:#000}#imagesRow{mso-line-height-rule:exactly;line-height:64px}#imagesRow p{margin:0;padding:0}#titleGroupHeading{font-size:42px;font-family:wf_segoe-ui_light,'Segoe UI Light','Segoe WP Light','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;color:#333;text-align:left}#titleAddedBy{font-size:17px;color:#333;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;text-align:left}#titleGroupDescription{font-size:14px;color:#333;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;text-align:left}#subscribeButtonH3{font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:28px;font-weight:normal;text-align:left}#subscribeSubHeadingAfterButton{font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:14px;color:#333}#secondaryActionHeading{font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:21px;color:#333;text-align:left}#subscribeButtonAnchor{display:block;width:100%;text-align:left}#groupTypeText{font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:12px;color:#333}#footerCell{font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:14px;padding-bottom:10px}.actionItemTable{table-layout:fixed}.actionItemContainer{text-decoration:none;text-align:left;overflow:hidden;padding:0 10px}.actionItemHeading{font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:21px;color:#333;line-height:21px;mso-line-height-rule:exactly}.actionItemText{font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:14px;color:#333;overflow:hidden;white-space:nowrap;display:block;text-overflow:ellipsis;white-space:pre-wrap;word-wrap:break-word;word-break:break-all;white-space:normal}.footerPadding{padding:10px 0 0 10px}.actionItemBorder{border:1px solid #ccc;-moz-box-sizing:border-box;-webkit-box-sizing:border-box;box-sizing:border-box}.secondaryActionLink{font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:14px;margin:0 15px}body,#bodyTable{direction:");
			writer.Write(data.NormalTextFlowDirectionCss);
			writer.Write("}.thumbsAlign{direction:");
			writer.Write(data.ReverseTextFlowDirectionCss);
			writer.Write("}@media only screen and (max-width:480px){#bodyTable{width:300px}}</style><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" width=\"100%\" id=\"bodyTable\" style=\"height:100%!important;width:100%!important;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;border-collapse:collapse;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td align=\"left\" valign=\"top\" id=\"bodyCell\" style=\"height:100%!important;width:100%!important;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;margin:0;padding:0\"> <table id=\"containerTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" width=\"100%\" align=\"left\" class=\"max-wide-table\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;max-width:600px;margin:0 auto;padding:0;border:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td id=\"contianerCell\" align=\"center\" valign=\"top\" class=\"segeo-font\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;color:#000;margin:0;padding:0\"> <table id=\"contentTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" width=\"100%\" bgcolor=\"#FFFFFF\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\">");
			if (data.GroupHasPhoto)
			{
				writer.Write(string.Empty);
				writer.Write(" <tr id=\"titelImgsRow\" style=\"border-spacing:0;margin:0;padding:0\"><td id=\"titleImgsCell\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <table id=\"imagesTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" width=\"100%\" align=\"left\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td colspan=\"3\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <img border=\"0\" width=\"100\" height=\"20\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></td> </tr><tr id=\"imagesRow\" style=\"border-spacing:0;mso-line-height-rule:exactly;line-height:64px;margin:0;padding:0\"><td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> <td style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <p class=\"thumbsAlign\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;margin:0;padding:0\"><img border=\"0\" width=\"70\" height=\"70\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"");
				writer.Write(data.GroupPhotoId);
				writer.Write("\" /></p> </td> <td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> </tr><tr style=\"border-spacing:0;margin:0;padding:0\"><td colspan=\"3\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <img border=\"0\" width=\"100\" height=\"20\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></td> </tr></table></td> </tr>");
			}
			writer.Write(string.Empty);
			writer.Write(" <tr id=\"titleTextRow\" style=\"border-spacing:0;margin:0;padding:0\"><td id=\"titleTextCell\" align=\"left\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;margin:0;padding:0\"> <table id=\"titlesTable\" border=\"0\" cellpadding=\"10\" cellspacing=\"0\" width=\"100%\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> <td align=\"left\" class=\"segeo-font left-align\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;margin:0;padding:0\"><span id=\"titleGroupHeading\" class=\"left-align\" style=\"font-family:wf_segoe-ui_light,'Segoe UI Light','Segoe WP Light','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;text-align:left;font-size:42px;color:#333\">");
			writer.Write(data.GroupTitleHeading);
			writer.Write("</span></td> <td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> </tr><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> <td style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:25px 0 0\"> <div id=\"titleAddedBy\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:17px;color:#333;text-align:left\" align=\"left\">");
			writer.Write(data.JoiningHeaderMessage);
			writer.Write("</div> </td> <td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> </tr><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> <td align=\"left\" class=\"segeo-font left-align\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;margin:0;padding:0\"> <p id=\"titleGroupDescription\" class=\"left-align\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-align:left;font-size:14px;color:#333;margin:1em 0\" align=\"left\">");
			writer.Write(data.GroupDescription);
			writer.Write("</p> </td> <td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> </tr>");
			if (data.ShowSubscribeBody)
			{
				writer.Write(string.Empty);
				writer.Write(" <tr style=\"border-spacing:0;margin:0;padding:0\"><td colspan=\"3\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <img border=\"0\" width=\"100\" height=\"40\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></td> </tr>");
			}
			writer.Write(string.Empty);
			writer.Write(" </table></td> </tr>");
			if (data.ShowSubscribeBody)
			{
				writer.Write(string.Empty);
				writer.Write(" <tr id=\"groupActivityRow\" bgcolor=\"#F4F4F4\" style=\"border-spacing:0;margin:0;padding:0\"><td id=\"groupActivityCell\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <table id=\"groupActivityTable\" border=\"0\" cellpadding=\"10\" cellspacing=\"0\" width=\"100%\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> <td align=\"left\" class=\"left-align\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;margin:0;padding:0\"> <img border=\"0\" width=\"20\" height=\"40\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /><div style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif\"><font color=\"#F58368\"><a id=\"subscribeButtonAnchor\" href=\"");
				writer.Write(data.SubscribeUrl);
				writer.Write("\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-decoration:none!important;display:block;width:100%;text-align:left\"><font id=\"subscribeButtonH3\" color=\"#0075C2\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:28px;font-weight:normal;text-align:left\">");
				writer.Write(data.SubscribeActivityToInboxLabel);
				writer.Write("</font>");
				if (data.IsLeftToRightLanguage)
				{
					writer.Write(string.Empty);
					writer.Write("<img id=\"subscribeButtonArrow\" alt=\"\" height=\"30\" width=\"30\" src=\"");
					writer.Write(data.ArrowImageId);
					writer.Write("\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0 none\" />");
				}
				writer.Write(string.Empty);
				writer.Write(" ");
				if (data.IsRightToLeftLanguage)
				{
					writer.Write(string.Empty);
					writer.Write("<img id=\"subscribeButtonArrow\" alt=\"\" height=\"30\" width=\"30\" src=\"");
					writer.Write(data.FlippedArrowImageId);
					writer.Write("\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0 none\" />");
				}
				writer.Write(string.Empty);
				writer.Write("</a></font></div> <img border=\"0\" width=\"20\" height=\"30\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /><div id=\"subscribeSubHeadingAfterButton\" class=\"left-align\" style=\"font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;text-align:left;font-size:14px;color:#333\" align=\"left\">");
				writer.Write(data.SubscribeActivityToInboxDescriptionLabel);
				writer.Write("</div> </td> <td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> </tr><tr style=\"border-spacing:0;margin:0;padding:0\"><td colspan=\"3\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <img border=\"0\" width=\"100\" height=\"40\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></td> </tr></table></td> </tr>");
			}
			writer.Write(string.Empty);
			writer.Write(" <tr id=\"primaryActionsRow\" style=\"border-spacing:0;margin:0;padding:0\"><td id=\"primaryActionsCell\" align=\"center\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> <td align=\"center\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <div style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif\"><img border=\"0\" width=\"1\" height=\"20\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></div> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"actionItemTable\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;table-layout:fixed;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"70\" valign=\"top\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><img border=\"0\" width=\"70\" height=\"70\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"");
			writer.Write(data.ConverasationsImageId);
			writer.Write("\" /></td> <td align=\"left\" class=\"actionItemContainer left-align\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;text-decoration:none;overflow:hidden;margin:0;padding:0 10px\"><span class=\"actionItemHeading\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:21px;color:#333;line-height:21px;mso-line-height-rule:exactly\"><a href=\"mailto:");
			writer.Write(data.GroupSmtpAddress);
			writer.Write("\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-decoration:none!important\"><font color=\"#000000\" class=\"left-align\" style=\"text-align:left\">");
			writer.Write(data.StartConversationLabel);
			writer.Write("</font></a></span> <div style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif\"><a href=\"mailto:");
			writer.Write(data.GroupSmtpAddress);
			writer.Write("\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-decoration:none!important\"><span class=\"left-align actionItemText\" style=\"font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;text-align:left;font-size:14px;color:#333;overflow:hidden;display:block;text-overflow:ellipsis;word-wrap:break-word;word-break:break-all;white-space:normal\"><font color=\"#000000\">");
			writer.Write(data.StartConversationEmail);
			writer.Write("</font></span></a></div> </td> </tr></table>");
			if (data.IsSharePointEnabled)
			{
				writer.Write(string.Empty);
				writer.Write(" <div style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif\"><img border=\"0\" width=\"1\" height=\"20\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></div> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" align=\"center\" class=\"actionItemTable\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;table-layout:fixed;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"70\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><img border=\"0\" width=\"70\" height=\"70\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"");
				writer.Write(data.FilesImageId);
				writer.Write("\" /></td> <td align=\"left\" class=\"actionItemContainer left-align\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;text-decoration:none;overflow:hidden;margin:0;padding:0 10px\"><span class=\"actionItemHeading\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:21px;color:#333;line-height:21px;mso-line-height-rule:exactly\"><a href=\"");
				writer.Write(data.SharePointUrl);
				writer.Write("\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-decoration:none!important\"><font color=\"#000000\" class=\"left-align\" style=\"text-align:left\">");
				writer.Write(data.GroupDocumentsMainTitleHeaderLabel);
				writer.Write("</font></a></span> <div style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif\"><a href=\"");
				writer.Write(data.SharePointUrl);
				writer.Write("\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-decoration:none!important\"><span class=\"left-align actionItemText\" style=\"font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;text-align:left;font-size:14px;color:#333;overflow:hidden;display:block;text-overflow:ellipsis;word-wrap:break-word;word-break:break-all;white-space:normal\"><font color=\"#000000\" class=\"left-align\" style=\"text-align:left\">");
				writer.Write(data.GroupDocumentsMainDescriptionHeaderLabel);
				writer.Write("</font></span></a></div> </td> </tr></table>");
			}
			writer.Write(string.Empty);
			writer.Write(" </td> <td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> </tr></table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td colspan=\"3\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><img border=\"0\" width=\"1\" height=\"20\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></td> </tr><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> <td style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> <div id=\"groupTypeText\" class=\"left-align\" style=\"font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;text-align:left;font-size:12px;color:#333\" align=\"left\">");
			writer.Write(data.GroupTypeStringLabel);
			writer.Write("</div> </td> <td width=\"20\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> </tr><tr style=\"border-spacing:0;margin:0;padding:0\"><td colspan=\"3\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><img border=\"0\" width=\"1\" height=\"40\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></td> </tr></table></td> </tr><tr id=\"secondaryActionRow\" style=\"border-spacing:0;margin:0;padding:0\"><td id=\"secondaryActionCell\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\">");
			if (data.ShowExchangeLinks)
			{
				writer.Write(string.Empty);
				writer.Write(" <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td width=\"10\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> <td width=\"100\" align=\"center\" valign=\"middle\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><img border=\"0\" width=\"80\" height=\"30\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"");
				writer.Write(data.O365ImageId);
				writer.Write("\" /><img border=\"0\" width=\"100\" height=\"1\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></td> <td class=\"left-align\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;margin:0;padding:0\" align=\"left\"> <div id=\"secondaryActionHeading\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:21px;color:#333;text-align:left\" align=\"left\">");
				writer.Write(data.CollaborateHeadingLabel);
				writer.Write("</div><a href=\"");
				writer.Write(data.InboxUrl);
				writer.Write("\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-decoration:none!important\"><font color=\"#228ECD\" class=\"left-align\" style=\"text-align:left\">");
				writer.Write(data.GroupConversationsLabel);
				writer.Write(" </font></a><a href=\"");
				writer.Write(data.CalendarUrl);
				writer.Write("\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-decoration:none!important\"><font color=\"#228ECD\" class=\"left-align\" style=\"text-align:left\">");
				writer.Write(data.GroupCalendarLabel);
				writer.Write(" </font></a> </td> <td width=\"10\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"> </td> </tr><tr style=\"border-spacing:0;margin:0;padding:0\"><td style=\"mso-line-height-rule:exactly;line-height:20px;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><img border=\"0\" width=\"1\" height=\"20\" alt=\"\" style=\"outline:0;text-decoration:none;-ms-interpolation-mode:bicubic;border:0\" src=\"cid:blank\" /></td> </tr></table>");
			}
			writer.Write(string.Empty);
			writer.Write(" </td> </tr></table></td> </tr><tr style=\"border-spacing:0;margin:0;padding:0\"><td id=\"footerCell\" align=\"left\" valign=\"top\" bgcolor=\"#F4F4F4\" class=\"segeo-font\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;font-family:wf_segoe-ui_normal,'Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;font-size:14px;margin:0;padding:0 0 10px\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border-collapse:collapse;font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;margin:0;padding:0\"><tr style=\"border-spacing:0;margin:0;padding:0\"><td align=\"left\" class=\"footerPadding\" style=\"mso-table-lspace:0;mso-table-rspace:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;border-spacing:0;text-align:left;margin:0;padding:10px 0 0 10px\"><span style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif\">");
			writer.Write(data.UnsubscribeFooterPrefixLabel);
			writer.Write(" </span><a href=\"");
			writer.Write(data.UnsubscribeUrl);
			writer.Write("\" style=\"font-family:wf_segoe-ui_semilight,'Segoe UI Semilight','Segoe WP Semilight','Segoe UI','Segoe WP',Tahoma,Arial,sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;text-decoration:none!important\">");
			writer.Write(data.UnsubscribeFooterLabel);
			writer.Write(" </a></td> </tr></table></td> </tr></table></td> </tr></table></body> </html>");
		}
	}
}
