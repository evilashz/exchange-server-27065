using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F6 RID: 1526
	[DataServiceKey("objectId")]
	public class Policy : DirectoryObject
	{
		// Token: 0x06001AA2 RID: 6818 RVA: 0x0003148C File Offset: 0x0002F68C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Policy CreatePolicy(string objectId, Collection<string> policyDetail)
		{
			Policy policy = new Policy();
			policy.objectId = objectId;
			if (policyDetail == null)
			{
				throw new ArgumentNullException("policyDetail");
			}
			policy.policyDetail = policyDetail;
			return policy;
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x000314BC File Offset: 0x0002F6BC
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x000314C4 File Offset: 0x0002F6C4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string displayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x000314CD File Offset: 0x0002F6CD
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x000314D5 File Offset: 0x0002F6D5
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? policyType
		{
			get
			{
				return this._policyType;
			}
			set
			{
				this._policyType = value;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x000314DE File Offset: 0x0002F6DE
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x000314E6 File Offset: 0x0002F6E6
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> policyDetail
		{
			get
			{
				return this._policyDetail;
			}
			set
			{
				this._policyDetail = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000314EF File Offset: 0x0002F6EF
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x000314F7 File Offset: 0x0002F6F7
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? tenantDefaultPolicy
		{
			get
			{
				return this._tenantDefaultPolicy;
			}
			set
			{
				this._tenantDefaultPolicy = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00031500 File Offset: 0x0002F700
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00031508 File Offset: 0x0002F708
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> policyAppliedTo
		{
			get
			{
				return this._policyAppliedTo;
			}
			set
			{
				if (value != null)
				{
					this._policyAppliedTo = value;
				}
			}
		}

		// Token: 0x04001C1E RID: 7198
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001C1F RID: 7199
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _policyType;

		// Token: 0x04001C20 RID: 7200
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _policyDetail = new Collection<string>();

		// Token: 0x04001C21 RID: 7201
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _tenantDefaultPolicy;

		// Token: 0x04001C22 RID: 7202
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _policyAppliedTo = new Collection<DirectoryObject>();
	}
}
