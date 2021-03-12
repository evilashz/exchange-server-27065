using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000104 RID: 260
	[Cmdlet("get", "User", DefaultParameterSetName = "Identity")]
	public sealed class GetUser : GetADUserBase<UserIdParameter>
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00045664 File Offset: 0x00043864
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				if (this.Arbitration)
				{
					return GetUser.ArbitrationAllowedRecipientTypeDetails;
				}
				if (this.PublicFolder)
				{
					return GetUser.PublicFolderAllowedRecipientTypeDetails;
				}
				if (this.AuditLog)
				{
					return GetUser.AuditLogAllowedRecipientTypeDetails;
				}
				return this.RecipientTypeDetails ?? GetUser.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x000456B9 File Offset: 0x000438B9
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x000456DF File Offset: 0x000438DF
		[Parameter]
		public SwitchParameter Arbitration
		{
			get
			{
				return (SwitchParameter)(base.Fields["Arbitration"] ?? false);
			}
			set
			{
				base.Fields["Arbitration"] = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x000456F7 File Offset: 0x000438F7
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x0004571D File Offset: 0x0004391D
		[Parameter(Mandatory = false)]
		public SwitchParameter PublicFolder
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublicFolder"] ?? false);
			}
			set
			{
				base.Fields["PublicFolder"] = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00045735 File Offset: 0x00043935
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x0004575B File Offset: 0x0004395B
		[Parameter(Mandatory = false)]
		public SwitchParameter AuditLog
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuditLog"] ?? false);
			}
			set
			{
				base.Fields["AuditLog"] = value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00045773 File Offset: 0x00043973
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x0004578A File Offset: 0x0004398A
		[Parameter]
		public NetID ConsumerNetID
		{
			get
			{
				return (NetID)base.Fields[ADUserSchema.ConsumerNetID];
			}
			set
			{
				base.Fields[ADUserSchema.ConsumerNetID] = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0004579D File Offset: 0x0004399D
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x000457B4 File Offset: 0x000439B4
		[Parameter]
		[ValidateNotNullOrEmpty]
		public RecipientTypeDetails[] RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails[])base.Fields["RecipientTypeDetails"];
			}
			set
			{
				base.VerifyValues<RecipientTypeDetails>(GetUser.PublicFolderAllowedRecipientTypeDetails.Union(GetUser.AllowedRecipientTypeDetails).ToArray<RecipientTypeDetails>(), value);
				base.Fields["RecipientTypeDetails"] = value;
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000457E4 File Offset: 0x000439E4
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser aduser = (ADUser)dataObject;
			if (null != aduser.MasterAccountSid)
			{
				aduser.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(aduser.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				aduser.ResetChangeTracking();
			}
			return new User(aduser);
		}

		// Token: 0x040003A9 RID: 937
		internal static readonly RecipientTypeDetails[] ArbitrationAllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.ArbitrationMailbox
		};

		// Token: 0x040003AA RID: 938
		internal static readonly RecipientTypeDetails[] PublicFolderAllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.PublicFolderMailbox
		};

		// Token: 0x040003AB RID: 939
		internal static readonly RecipientTypeDetails[] MonitoringAllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MonitoringMailbox
		};

		// Token: 0x040003AC RID: 940
		internal static readonly RecipientTypeDetails[] AuditLogAllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.AuditLogMailbox
		};

		// Token: 0x040003AD RID: 941
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RoomMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.LinkedRoomMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.DisabledUser,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.EquipmentMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.LegacyMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.LinkedMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.UserMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailUser,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.SharedMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.TeamMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.GroupMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.DiscoveryMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.User,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.LinkedUser,
			(RecipientTypeDetails)((ulong)int.MinValue),
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RemoteRoomMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RemoteEquipmentMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RemoteSharedMailbox,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RemoteTeamMailbox
		};
	}
}
