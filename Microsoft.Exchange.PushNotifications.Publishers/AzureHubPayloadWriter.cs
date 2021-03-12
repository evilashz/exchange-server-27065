using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200008B RID: 139
	internal class AzureHubPayloadWriter
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0000F960 File Offset: 0x0000DB60
		public void AddAuthorizationRule(AzureSasKey sasKey)
		{
			ArgumentValidator.ThrowIfNull("sasKey", sasKey);
			ArgumentValidator.ThrowIfNull("sasKey.Claims", sasKey.Claims);
			this.sasBuilder.AppendFormat("<AuthorizationRule i:type=\"SharedAccessAuthorizationRule\">\r\n            <ClaimType>SharedAccessKey</ClaimType>\r\n            <ClaimValue>None</ClaimValue>\r\n            <Rights>\r\n                {0}\r\n            </Rights>\r\n            <KeyName>{1}</KeyName>\r\n            <PrimaryKey>{2}</PrimaryKey>\r\n        </AuthorizationRule>", this.GetAccessRightXML(sasKey.Claims), sasKey.KeyName, sasKey.KeyValue.AsUnsecureString());
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000F9BB File Offset: 0x0000DBBB
		public override string ToString()
		{
			return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<entry xmlns=\"http://www.w3.org/2005/Atom\">\r\n    <content type=\"application/xml\">\r\n        <NotificationHubDescription xmlns=\"http://schemas.microsoft.com/netservices/2010/10/servicebus/connect\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n        <RegistrationTtl>P7D</RegistrationTtl>\r\n        <AuthorizationRules>\r\n        {0}\r\n        </AuthorizationRules>\r\n        </NotificationHubDescription>\r\n    </content>\r\n</entry>", this.sasBuilder.ToString());
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000F9D4 File Offset: 0x0000DBD4
		private string GetAccessRightXML(AzureSasKey.ClaimType claims)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((claims & AzureSasKey.ClaimType.Listen) == AzureSasKey.ClaimType.Listen)
			{
				stringBuilder.AppendFormat("<AccessRights>{0}</AccessRights>", AzureSasKey.ClaimType.Listen.ToString());
			}
			if ((claims & AzureSasKey.ClaimType.Send) == AzureSasKey.ClaimType.Send)
			{
				stringBuilder.AppendFormat("<AccessRights>{0}</AccessRights>", AzureSasKey.ClaimType.Send.ToString());
			}
			if ((claims & AzureSasKey.ClaimType.Manage) == AzureSasKey.ClaimType.Manage)
			{
				stringBuilder.AppendFormat("<AccessRights>{0}</AccessRights>", AzureSasKey.ClaimType.Manage.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000253 RID: 595
		public const string CreateHubXMLTemplate = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<entry xmlns=\"http://www.w3.org/2005/Atom\">\r\n    <content type=\"application/xml\">\r\n        <NotificationHubDescription xmlns=\"http://schemas.microsoft.com/netservices/2010/10/servicebus/connect\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n        <RegistrationTtl>P7D</RegistrationTtl>\r\n        <AuthorizationRules>\r\n        {0}\r\n        </AuthorizationRules>\r\n        </NotificationHubDescription>\r\n    </content>\r\n</entry>";

		// Token: 0x04000254 RID: 596
		public const string AuthorizationRuleXMLTemplate = "<AuthorizationRule i:type=\"SharedAccessAuthorizationRule\">\r\n            <ClaimType>SharedAccessKey</ClaimType>\r\n            <ClaimValue>None</ClaimValue>\r\n            <Rights>\r\n                {0}\r\n            </Rights>\r\n            <KeyName>{1}</KeyName>\r\n            <PrimaryKey>{2}</PrimaryKey>\r\n        </AuthorizationRule>";

		// Token: 0x04000255 RID: 597
		private const string AccessRightsXMLTemplate = "<AccessRights>{0}</AccessRights>";

		// Token: 0x04000256 RID: 598
		private readonly StringBuilder sasBuilder = new StringBuilder();
	}
}
