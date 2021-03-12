using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E0 RID: 4576
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DefaultPolicyCreationException : LocalizedException
	{
		// Token: 0x0600B93F RID: 47423 RVA: 0x002A5AAE File Offset: 0x002A3CAE
		public DefaultPolicyCreationException(string moreInfo) : base(Strings.DefaultPolicyCreation(moreInfo))
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x0600B940 RID: 47424 RVA: 0x002A5AC3 File Offset: 0x002A3CC3
		public DefaultPolicyCreationException(string moreInfo, Exception innerException) : base(Strings.DefaultPolicyCreation(moreInfo), innerException)
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x0600B941 RID: 47425 RVA: 0x002A5AD9 File Offset: 0x002A3CD9
		protected DefaultPolicyCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.moreInfo = (string)info.GetValue("moreInfo", typeof(string));
		}

		// Token: 0x0600B942 RID: 47426 RVA: 0x002A5B03 File Offset: 0x002A3D03
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("moreInfo", this.moreInfo);
		}

		// Token: 0x17003A3C RID: 14908
		// (get) Token: 0x0600B943 RID: 47427 RVA: 0x002A5B1E File Offset: 0x002A3D1E
		public string MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
		}

		// Token: 0x04006457 RID: 25687
		private readonly string moreInfo;
	}
}
