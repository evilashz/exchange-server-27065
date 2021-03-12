using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000BB RID: 187
	[Cmdlet("set", "RemoteMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRemoteMailbox : SetMailUserBase<RemoteMailboxIdParameter, RemoteMailbox>
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00030DA8 File Offset: 0x0002EFA8
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00030DBF File Offset: 0x0002EFBF
		[Parameter(Mandatory = false)]
		public ConvertibleRemoteMailboxSubType Type
		{
			get
			{
				return (ConvertibleRemoteMailboxSubType)base.Fields["Type"];
			}
			set
			{
				base.Fields["Type"] = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00030DD7 File Offset: 0x0002EFD7
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x00030DFD File Offset: 0x0002EFFD
		[Parameter(Mandatory = false)]
		public SwitchParameter ACLableSyncedObjectEnabled
		{
			get
			{
				return (SwitchParameter)(base.Fields["ACLableSyncedObjectEnabled"] ?? false);
			}
			set
			{
				base.Fields["ACLableSyncedObjectEnabled"] = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00030E15 File Offset: 0x0002F015
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRemoteMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00030E27 File Offset: 0x0002F027
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x00030E2F File Offset: 0x0002F02F
		private new SwitchParameter BypassLiveId
		{
			get
			{
				return base.BypassLiveId;
			}
			set
			{
				base.BypassLiveId = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00030E38 File Offset: 0x0002F038
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x00030E40 File Offset: 0x0002F040
		private new string FederatedIdentity
		{
			get
			{
				return base.FederatedIdentity;
			}
			set
			{
				base.FederatedIdentity = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00030E49 File Offset: 0x0002F049
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x00030E51 File Offset: 0x0002F051
		private new NetID NetID
		{
			get
			{
				return base.NetID;
			}
			set
			{
				base.NetID = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00030E5A File Offset: 0x0002F05A
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x00030E62 File Offset: 0x0002F062
		private new Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
			set
			{
				base.SKUCapability = value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00030E6B File Offset: 0x0002F06B
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x00030E73 File Offset: 0x0002F073
		private new MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
			set
			{
				base.AddOnSKUCapability = value;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00030E7C File Offset: 0x0002F07C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			if (base.Fields.IsModified("Type"))
			{
				RemoteMailboxType type = (RemoteMailboxType)this.Type;
				aduser.UpdateRemoteMailboxType(type, this.ACLableSyncedObjectEnabled);
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00030ECB File Offset: 0x0002F0CB
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return RemoteMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x0400028A RID: 650
		public const string ParameterACLableSyncedEnabled = "ACLableSyncedObjectEnabled";
	}
}
