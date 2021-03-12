using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000507 RID: 1287
	[KnownType(typeof(OwaMailboxPolicyFeatureInfo))]
	[DataContract]
	[KnownType(typeof(UMMailboxFeatureInfo))]
	[KnownType(typeof(LitigationHoldFeatureInfo))]
	[KnownType(typeof(EASMailboxFeatureInfo))]
	public abstract class MailboxFeatureInfo : BaseRow
	{
		// Token: 0x06003DD0 RID: 15824 RVA: 0x000B9E18 File Offset: 0x000B8018
		public MailboxFeatureInfo(Identity id) : base(id, null)
		{
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000B9E22 File Offset: 0x000B8022
		public MailboxFeatureInfo(ADObject objectId) : base(objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			this.UseModalDialogForEdit = true;
			this.UseModalDialogForEnable = false;
		}

		// Token: 0x17002442 RID: 9282
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x000B9E47 File Offset: 0x000B8047
		// (set) Token: 0x06003DD3 RID: 15827 RVA: 0x000B9E4F File Offset: 0x000B804F
		[DataMember]
		public string Name { get; protected set; }

		// Token: 0x17002443 RID: 9283
		// (get) Token: 0x06003DD4 RID: 15828 RVA: 0x000B9E58 File Offset: 0x000B8058
		// (set) Token: 0x06003DD5 RID: 15829 RVA: 0x000B9E60 File Offset: 0x000B8060
		[DataMember]
		public string EnableCommandUrl { get; protected set; }

		// Token: 0x17002444 RID: 9284
		// (get) Token: 0x06003DD6 RID: 15830 RVA: 0x000B9E69 File Offset: 0x000B8069
		// (set) Token: 0x06003DD7 RID: 15831 RVA: 0x000B9E71 File Offset: 0x000B8071
		[DataMember]
		public string EditCommandUrl { get; protected set; }

		// Token: 0x17002445 RID: 9285
		// (get) Token: 0x06003DD8 RID: 15832 RVA: 0x000B9E7A File Offset: 0x000B807A
		// (set) Token: 0x06003DD9 RID: 15833 RVA: 0x000B9E82 File Offset: 0x000B8082
		[DataMember]
		public int? EnableWizardDialogHeight { get; protected set; }

		// Token: 0x17002446 RID: 9286
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x000B9E8B File Offset: 0x000B808B
		// (set) Token: 0x06003DDB RID: 15835 RVA: 0x000B9E93 File Offset: 0x000B8093
		[DataMember]
		public int? EnableWizardDialogWidth { get; protected set; }

		// Token: 0x17002447 RID: 9287
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x000B9E9C File Offset: 0x000B809C
		// (set) Token: 0x06003DDD RID: 15837 RVA: 0x000B9EA4 File Offset: 0x000B80A4
		[DataMember]
		public int? PropertiesDialogHeight { get; protected set; }

		// Token: 0x17002448 RID: 9288
		// (get) Token: 0x06003DDE RID: 15838 RVA: 0x000B9EAD File Offset: 0x000B80AD
		// (set) Token: 0x06003DDF RID: 15839 RVA: 0x000B9EB5 File Offset: 0x000B80B5
		[DataMember]
		public int? PropertiesDialogWidth { get; protected set; }

		// Token: 0x17002449 RID: 9289
		// (get) Token: 0x06003DE0 RID: 15840 RVA: 0x000B9EBE File Offset: 0x000B80BE
		// (set) Token: 0x06003DE1 RID: 15841 RVA: 0x000B9EC6 File Offset: 0x000B80C6
		[DataMember]
		public virtual string Status { get; protected set; }

		// Token: 0x1700244A RID: 9290
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x000B9ECF File Offset: 0x000B80CF
		// (set) Token: 0x06003DE3 RID: 15843 RVA: 0x000B9ED7 File Offset: 0x000B80D7
		[DataMember]
		public virtual bool CanChangeStatus { get; protected set; }

		// Token: 0x1700244B RID: 9291
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x000B9EE0 File Offset: 0x000B80E0
		// (set) Token: 0x06003DE5 RID: 15845 RVA: 0x000B9EE8 File Offset: 0x000B80E8
		[DataMember]
		public virtual string SpriteId { get; protected set; }

		// Token: 0x1700244C RID: 9292
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x000B9EF1 File Offset: 0x000B80F1
		// (set) Token: 0x06003DE7 RID: 15847 RVA: 0x000B9EF9 File Offset: 0x000B80F9
		[DataMember]
		public virtual string IconAltText { get; protected set; }

		// Token: 0x1700244D RID: 9293
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x000B9F02 File Offset: 0x000B8102
		// (set) Token: 0x06003DE9 RID: 15849 RVA: 0x000B9F0A File Offset: 0x000B810A
		[DataMember]
		public bool UseModalDialogForEdit { get; protected set; }

		// Token: 0x1700244E RID: 9294
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x000B9F13 File Offset: 0x000B8113
		// (set) Token: 0x06003DEB RID: 15851 RVA: 0x000B9F1B File Offset: 0x000B811B
		[DataMember]
		public bool UseModalDialogForEnable { get; protected set; }

		// Token: 0x1700244F RID: 9295
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000B9F24 File Offset: 0x000B8124
		public virtual bool Visible
		{
			get
			{
				return !this.IsReadOnly || this.ShowReadOnly;
			}
		}

		// Token: 0x17002450 RID: 9296
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x000B9F38 File Offset: 0x000B8138
		protected bool IsReadOnly
		{
			get
			{
				bool flag = this.Status == ClientStrings.EnabledDisplayText && this.EditCommandUrl != null;
				return !this.CanChangeStatus && !flag;
			}
		}

		// Token: 0x17002451 RID: 9297
		// (get) Token: 0x06003DEE RID: 15854 RVA: 0x000B9F75 File Offset: 0x000B8175
		// (set) Token: 0x06003DEF RID: 15855 RVA: 0x000B9F7D File Offset: 0x000B817D
		protected bool ShowReadOnly { get; set; }

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000B9F86 File Offset: 0x000B8186
		internal static string GetStatusText(bool isEnabled)
		{
			if (!isEnabled)
			{
				return ClientStrings.DisabledDisplayText;
			}
			return ClientStrings.EnabledDisplayText;
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000B9F98 File Offset: 0x000B8198
		internal static bool? IsEnabled(string statusText)
		{
			if (statusText == ClientStrings.EnabledPendingDisplayText)
			{
				return new bool?(true);
			}
			if (statusText == ClientStrings.DisabledPendingDisplayText)
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x000B9FD8 File Offset: 0x000B81D8
		protected bool IsInRole(string[] roles)
		{
			if (roles == null)
			{
				throw new ArgumentException("roles is null");
			}
			foreach (string role in roles)
			{
				if (!RbacPrincipal.Current.IsInRole(role))
				{
					return false;
				}
			}
			return true;
		}
	}
}
