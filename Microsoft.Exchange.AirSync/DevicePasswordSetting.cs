using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200006E RID: 110
	internal class DevicePasswordSetting : SettingsBase
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00022C41 File Offset: 0x00020E41
		internal DevicePasswordSetting(XmlNode request, XmlNode response, IAirSyncUser user, GlobalInfo globalInfo, ProtocolLogger protocolLogger) : base(request, response, protocolLogger)
		{
			this.user = user;
			if (globalInfo == null)
			{
				throw new ArgumentNullException("globalInfo should not be null here!");
			}
			this.globalInfo = globalInfo;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00022C74 File Offset: 0x00020E74
		public override void Execute()
		{
			using (this.user.Context.Tracker.Start(TimeId.DevicePasswordExecute))
			{
				XmlNode firstChild = base.Request.FirstChild;
				string localName;
				if ((localName = firstChild.LocalName) != null && localName == "Set")
				{
					this.ProcessSet(firstChild);
				}
				this.ReportStatus();
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00022CE8 File Offset: 0x00020EE8
		private void ReportStatus()
		{
			XmlNode xmlNode = base.Response.OwnerDocument.CreateElement("Status", "Settings:");
			int num = (int)this.status;
			xmlNode.InnerText = num.ToString(CultureInfo.InvariantCulture);
			base.Response.AppendChild(xmlNode);
			Command.CurrentCommand.PartialFailure = (this.status != SettingsBase.ErrorCode.Success);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00022D4C File Offset: 0x00020F4C
		private void ProcessSet(XmlNode setNode)
		{
			using (this.user.Context.Tracker.Start(TimeId.DevicePasswordProcessSet))
			{
				if (setNode.ChildNodes.Count != 1)
				{
					this.status = SettingsBase.ErrorCode.ProtocolError;
				}
				else
				{
					string innerText = setNode.FirstChild.InnerText;
					if (innerText.Length > 255)
					{
						this.status = SettingsBase.ErrorCode.InvalidArguments;
					}
					else
					{
						PolicyData policyData = ADNotificationManager.GetPolicyData(this.user);
						bool flag = policyData != null && policyData.PasswordRecoveryEnabled;
						if (innerText.Length > 0 && flag)
						{
							this.globalInfo.RecoveryPassword = innerText;
						}
						else
						{
							this.globalInfo.RecoveryPassword = null;
						}
						if (innerText.Length > 0 && !flag)
						{
							this.status = SettingsBase.ErrorCode.DeniedByPolicy;
						}
						else
						{
							this.status = SettingsBase.ErrorCode.Success;
						}
					}
				}
			}
		}

		// Token: 0x0400040D RID: 1037
		private SettingsBase.ErrorCode status = SettingsBase.ErrorCode.ProtocolError;

		// Token: 0x0400040E RID: 1038
		private GlobalInfo globalInfo;

		// Token: 0x0400040F RID: 1039
		private IAirSyncUser user;
	}
}
