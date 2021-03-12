using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200011C RID: 284
	internal class RightsManagementInformationSetting : SettingsBase
	{
		// Token: 0x06000EFA RID: 3834 RVA: 0x00055A90 File Offset: 0x00053C90
		public RightsManagementInformationSetting(XmlNode request, XmlNode response, IAirSyncUser user, CultureInfo cultureInfo, ProtocolLogger protocolLogger, MailboxLogger mailboxLogger) : base(request, response, protocolLogger)
		{
			this.user = user;
			this.cultureInfo = cultureInfo;
			this.mailboxLogger = mailboxLogger;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00055ABC File Offset: 0x00053CBC
		public override void Execute()
		{
			using (this.user.Context.Tracker.Start(TimeId.RMSExecute))
			{
				XmlNode firstChild = base.Request.FirstChild;
				string localName;
				if ((localName = firstChild.LocalName) != null && localName == "Get")
				{
					this.ProcessGet();
				}
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00055B28 File Offset: 0x00053D28
		private void ProcessGet()
		{
			using (this.user.Context.Tracker.Start(TimeId.RMSProcessGet))
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Processing RightsManagementInformationSetting - Get");
				XmlNode xmlNode = base.Response.OwnerDocument.CreateElement("Get", "Settings:");
				XmlNode xmlNode2 = base.Response.OwnerDocument.CreateElement("RightsManagementTemplates", "RightsManagement:");
				try
				{
					if (this.user.IrmEnabled)
					{
						List<RmsTemplate> list = new List<RmsTemplate>(RmsTemplateReaderCache.GetRmsTemplates(this.user.OrganizationId));
						IComparer<RmsTemplate> comparer = new RightsManagementInformationSetting.RmsTemplateNameComparer(this.cultureInfo);
						list.Sort(comparer);
						int count = list.Count;
						int maxRmsTemplates = GlobalSettings.MaxRmsTemplates;
						if (count > maxRmsTemplates)
						{
							list.RemoveRange(maxRmsTemplates, count - maxRmsTemplates);
						}
						using (List<RmsTemplate>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								RmsTemplate rmsTemplate = enumerator.Current;
								AirSyncDiagnostics.TraceInfo<Guid>(ExTraceGlobals.RequestsTracer, this, "Found RMS template {0}", rmsTemplate.Id);
								XmlNode xmlNode3 = base.Response.OwnerDocument.CreateElement("RightsManagementTemplate", "RightsManagement:");
								XmlNode xmlNode4 = base.Response.OwnerDocument.CreateElement("TemplateID", "RightsManagement:");
								xmlNode4.InnerText = rmsTemplate.Id.ToString();
								xmlNode3.AppendChild(xmlNode4);
								XmlNode xmlNode5 = base.Response.OwnerDocument.CreateElement("TemplateName", "RightsManagement:");
								xmlNode5.InnerText = rmsTemplate.GetName(this.cultureInfo);
								xmlNode3.AppendChild(xmlNode5);
								XmlNode xmlNode6 = base.Response.OwnerDocument.CreateElement("TemplateDescription", "RightsManagement:");
								xmlNode6.InnerText = rmsTemplate.GetDescription(this.cultureInfo);
								xmlNode3.AppendChild(xmlNode6);
								xmlNode2.AppendChild(xmlNode3);
							}
							goto IL_205;
						}
					}
					AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, this, "IRM feature disabled for user {0}", this.user.DisplayName);
					this.status = StatusCode.IRM_FeatureDisabled;
					IL_205:;
				}
				catch (AirSyncPermanentException ex)
				{
					AirSyncDiagnostics.TraceError<AirSyncPermanentException>(ExTraceGlobals.RequestsTracer, this, "AirSyncPermanentException encountered while processing RightsManagementInformationSetting->Get {0}", ex);
					if (base.ProtocolLogger != null && !string.IsNullOrEmpty(ex.ErrorStringForProtocolLogger))
					{
						base.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.Error, ex.ErrorStringForProtocolLogger);
					}
					this.ProcessException(ex);
					this.status = ex.AirSyncStatusCode;
				}
				XmlNode xmlNode7 = base.Response.OwnerDocument.CreateElement("Status", "Settings:");
				XmlNode xmlNode8 = xmlNode7;
				int num = (int)this.status;
				xmlNode8.InnerText = num.ToString(CultureInfo.InvariantCulture);
				base.Response.AppendChild(xmlNode7);
				xmlNode.AppendChild(xmlNode2);
				base.Response.AppendChild(xmlNode);
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Done processing RightsManagementInformationSetting - Get.");
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00055E58 File Offset: 0x00054058
		private void ProcessException(Exception exception)
		{
			using (this.user.Context.Tracker.Start(TimeId.RMSProcessException))
			{
				Command.CurrentCommand.PartialFailure = true;
				if (this.mailboxLogger != null)
				{
					RightsManagementException ex = exception as RightsManagementException;
					if (ex == null)
					{
						ex = (exception.InnerException as RightsManagementException);
					}
					if (ex != null)
					{
						this.mailboxLogger.SetData(MailboxLogDataName.IRM_FailureCode, ex.FailureCode);
					}
					this.mailboxLogger.SetData(MailboxLogDataName.IRM_Exception, new AirSyncUtility.ExceptionToStringHelper(exception));
				}
			}
		}

		// Token: 0x04000A46 RID: 2630
		private StatusCode status = StatusCode.Success;

		// Token: 0x04000A47 RID: 2631
		private IAirSyncUser user;

		// Token: 0x04000A48 RID: 2632
		private CultureInfo cultureInfo;

		// Token: 0x04000A49 RID: 2633
		private MailboxLogger mailboxLogger;

		// Token: 0x0200011D RID: 285
		private class RmsTemplateNameComparer : IComparer<RmsTemplate>
		{
			// Token: 0x06000EFE RID: 3838 RVA: 0x00055EF0 File Offset: 0x000540F0
			internal RmsTemplateNameComparer(CultureInfo locale)
			{
				this.locale = locale;
			}

			// Token: 0x06000EFF RID: 3839 RVA: 0x00055F00 File Offset: 0x00054100
			public int Compare(RmsTemplate template1, RmsTemplate template2)
			{
				if (template1 == template2)
				{
					return 0;
				}
				if (template1 == RmsTemplate.DoNotForward)
				{
					return -1;
				}
				if (template2 == RmsTemplate.DoNotForward)
				{
					return 1;
				}
				if (template1 == RmsTemplate.InternetConfidential)
				{
					return -1;
				}
				if (template2 == RmsTemplate.InternetConfidential)
				{
					return 1;
				}
				return string.Compare(template1.GetName(this.locale), template2.GetName(this.locale), true, this.locale);
			}

			// Token: 0x04000A4A RID: 2634
			private CultureInfo locale;
		}
	}
}
