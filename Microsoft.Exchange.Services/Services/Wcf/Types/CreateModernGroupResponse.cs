using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B46 RID: 2886
	[DataContract(Name = "CreateModernGroupResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateModernGroupResponse : BaseJsonResponse
	{
		// Token: 0x060051CE RID: 20942 RVA: 0x0010AE44 File Offset: 0x00109044
		internal CreateModernGroupResponse(Group group, string warningMessage = null)
		{
			Guid guid = (Guid)group.Properties["ExchangeDirectoryObjectId"];
			string text = (string)group.Properties["Mail"];
			this.Persona = new Persona
			{
				PersonaId = IdConverter.PersonaIdFromADObjectId(guid),
				ADObjectId = guid,
				DisplayName = group.DisplayName,
				Alias = group.Alias,
				EmailAddress = new EmailAddressWrapper
				{
					EmailAddress = text,
					MailboxType = MailboxHelper.MailboxTypeType.GroupMailbox.ToString(),
					Name = text,
					RoutingType = "SMTP"
				},
				PersonaType = PersonaTypeConverter.ToString(PersonType.ModernGroup)
			};
			this.Warning = warningMessage;
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0010AF08 File Offset: 0x00109108
		internal CreateModernGroupResponse(GroupMailbox groupMailbox)
		{
			this.Persona = new Persona
			{
				PersonaId = IdConverter.PersonaIdFromADObjectId(groupMailbox.Guid),
				ADObjectId = groupMailbox.Guid,
				DisplayName = groupMailbox.DisplayName,
				Alias = groupMailbox.Alias,
				EmailAddress = new EmailAddressWrapper
				{
					EmailAddress = groupMailbox.PrimarySmtpAddress.ToString(),
					MailboxType = MailboxHelper.MailboxTypeType.GroupMailbox.ToString(),
					Name = groupMailbox.DisplayName,
					RoutingType = "SMTP"
				},
				PersonaType = PersonaTypeConverter.ToString(PersonType.ModernGroup)
			};
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0010AFB8 File Offset: 0x001091B8
		internal CreateModernGroupResponse(string errorMessage)
		{
			this.Error = errorMessage;
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x060051D1 RID: 20945 RVA: 0x0010AFC7 File Offset: 0x001091C7
		// (set) Token: 0x060051D2 RID: 20946 RVA: 0x0010AFCF File Offset: 0x001091CF
		[DataMember(Name = "Persona", IsRequired = false)]
		public Persona Persona { get; set; }

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x060051D3 RID: 20947 RVA: 0x0010AFD8 File Offset: 0x001091D8
		// (set) Token: 0x060051D4 RID: 20948 RVA: 0x0010AFE0 File Offset: 0x001091E0
		[DataMember(Name = "Error", IsRequired = false)]
		public string Error { get; set; }

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x060051D5 RID: 20949 RVA: 0x0010AFE9 File Offset: 0x001091E9
		// (set) Token: 0x060051D6 RID: 20950 RVA: 0x0010AFF1 File Offset: 0x001091F1
		[DataMember(Name = "Warning", IsRequired = false)]
		public string Warning { get; set; }

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x0010AFFA File Offset: 0x001091FA
		// (set) Token: 0x060051D8 RID: 20952 RVA: 0x0010B002 File Offset: 0x00109202
		[DataMember(Name = "Logs", IsRequired = false)]
		public ModernGroupCreateLogEntry[] Logs { get; set; }
	}
}
