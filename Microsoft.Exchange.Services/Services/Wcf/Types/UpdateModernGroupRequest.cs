using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B44 RID: 2884
	[DataContract(Name = "UpdateModernGroupRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateModernGroupRequest : ComposeModernGroupRequestBase
	{
		// Token: 0x060051B1 RID: 20913 RVA: 0x0010AB57 File Offset: 0x00108D57
		public UpdateModernGroupRequest(bool isOwner)
		{
			this.IsOwner = isOwner;
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x060051B2 RID: 20914 RVA: 0x0010AB66 File Offset: 0x00108D66
		// (set) Token: 0x060051B3 RID: 20915 RVA: 0x0010AB6E File Offset: 0x00108D6E
		[DataMember(Name = "SmtpAddress", IsRequired = false)]
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			set
			{
				this.smtpAddress = value;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x060051B4 RID: 20916 RVA: 0x0010AB77 File Offset: 0x00108D77
		// (set) Token: 0x060051B5 RID: 20917 RVA: 0x0010AB89 File Offset: 0x00108D89
		[DataMember(Name = "DeletedMembers", IsRequired = false)]
		public string[] DeletedMembers
		{
			get
			{
				return this.deletedMembers ?? new string[0];
			}
			set
			{
				this.deletedMembers = value;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x060051B6 RID: 20918 RVA: 0x0010AB92 File Offset: 0x00108D92
		// (set) Token: 0x060051B7 RID: 20919 RVA: 0x0010ABA4 File Offset: 0x00108DA4
		[DataMember(Name = "DeletedOwners", IsRequired = false)]
		public string[] DeletedOwners
		{
			get
			{
				return this.deletedOwners ?? new string[0];
			}
			set
			{
				this.deletedOwners = value;
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x060051B8 RID: 20920 RVA: 0x0010ABAD File Offset: 0x00108DAD
		// (set) Token: 0x060051B9 RID: 20921 RVA: 0x0010ABB5 File Offset: 0x00108DB5
		[DataMember(Name = "RequireSenderAuthenticationEnabled", IsRequired = false)]
		public bool? RequireSenderAuthenticationEnabled { get; set; }

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x060051BA RID: 20922 RVA: 0x0010ABBE File Offset: 0x00108DBE
		// (set) Token: 0x060051BB RID: 20923 RVA: 0x0010ABC6 File Offset: 0x00108DC6
		[DataMember(Name = "AutoSubscribeNewGroupMembers", IsRequired = false)]
		public bool? AutoSubscribeNewGroupMembers { get; set; }

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x060051BC RID: 20924 RVA: 0x0010ABCF File Offset: 0x00108DCF
		// (set) Token: 0x060051BD RID: 20925 RVA: 0x0010ABD7 File Offset: 0x00108DD7
		[DataMember(Name = "CultureId", IsRequired = false)]
		public string CultureId { get; set; }

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x0010ABE0 File Offset: 0x00108DE0
		// (set) Token: 0x060051BF RID: 20927 RVA: 0x0010ABE8 File Offset: 0x00108DE8
		[DataMember(Name = "IsOwner", IsRequired = true)]
		public bool IsOwner { get; private set; }

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x060051C0 RID: 20928 RVA: 0x0010ABF1 File Offset: 0x00108DF1
		public ProxyAddress ProxyAddress
		{
			get
			{
				if (this.proxyAddress == null)
				{
					this.proxyAddress = new SmtpProxyAddress(this.smtpAddress, true);
				}
				return this.proxyAddress;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x060051C1 RID: 20929 RVA: 0x0010AC19 File Offset: 0x00108E19
		// (set) Token: 0x060051C2 RID: 20930 RVA: 0x0010AC21 File Offset: 0x00108E21
		internal CultureInfo Language { get; private set; }

		// Token: 0x060051C3 RID: 20931 RVA: 0x0010AC2A File Offset: 0x00108E2A
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x0010AC34 File Offset: 0x00108E34
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x0010AC38 File Offset: 0x00108E38
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.SmtpAddress))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (base.AddedMembers == null)
			{
				base.AddedMembers = new string[0];
			}
			if (base.AddedOwners == null)
			{
				base.AddedOwners = new string[0];
			}
			if (this.DeletedMembers == null)
			{
				this.DeletedMembers = new string[0];
			}
			if (this.DeletedOwners == null)
			{
				this.DeletedOwners = new string[0];
			}
			if (this.CultureId != null)
			{
				try
				{
					this.Language = CultureInfo.CreateSpecificCulture(this.CultureId);
				}
				catch (CultureNotFoundException innerException)
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(innerException), FaultParty.Sender);
				}
			}
			if (base.Name != null)
			{
				base.Name = base.Name.Trim();
			}
			if (base.Description != null)
			{
				base.Description = base.Description.Trim();
			}
			string[] second = base.AddedMembers.Intersect(this.DeletedMembers).ToArray<string>();
			base.AddedMembers = base.AddedMembers.Distinct<string>().Except(second).ToArray<string>();
			this.DeletedMembers = this.DeletedMembers.Distinct<string>().Except(second).ToArray<string>();
			string[] second2 = base.AddedOwners.Intersect(this.DeletedOwners).ToArray<string>();
			base.AddedOwners = base.AddedOwners.Distinct<string>().Except(second2).ToArray<string>();
			this.DeletedOwners = this.DeletedOwners.Distinct<string>().Except(second2).ToArray<string>();
		}

		// Token: 0x04002DA7 RID: 11687
		private string smtpAddress;

		// Token: 0x04002DA8 RID: 11688
		private ProxyAddress proxyAddress;

		// Token: 0x04002DA9 RID: 11689
		private string[] deletedMembers;

		// Token: 0x04002DAA RID: 11690
		private string[] deletedOwners;
	}
}
