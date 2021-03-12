using System;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000CD RID: 205
	public class SerializedSecurityContext : SoapHeader
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x00016DE9 File Offset: 0x00014FE9
		public SerializedSecurityContext()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00016DF4 File Offset: 0x00014FF4
		internal SerializedSecurityContext(SecurityAccessToken securityContext)
		{
			if (securityContext == null)
			{
				throw new ArgumentNullException("securityContext");
			}
			this.userSid = securityContext.UserSid;
			this.groupSids = this.GroupSidArrayFromSidStringAndAttributeArray(securityContext.GroupSids);
			this.restrictedGroupSids = this.GroupSidArrayFromSidStringAndAttributeArray(securityContext.RestrictedGroupSids);
			this.groupSAArray = securityContext.GroupSids;
			this.restrictedGroupSAArray = securityContext.RestrictedGroupSids;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00016E5D File Offset: 0x0001505D
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00016E65 File Offset: 0x00015065
		[XmlElement]
		public string UserSid
		{
			get
			{
				return this.userSid;
			}
			set
			{
				this.userSid = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00016E6E File Offset: 0x0001506E
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00016E76 File Offset: 0x00015076
		[XmlArrayItem("GroupIdentifier")]
		public GroupSid[] GroupSids
		{
			get
			{
				return this.groupSids;
			}
			set
			{
				this.groupSids = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00016E7F File Offset: 0x0001507F
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00016E87 File Offset: 0x00015087
		[XmlArrayItem("RestrictedGroupIdentifier")]
		public GroupSid[] RestrictedGroupSids
		{
			get
			{
				return this.restrictedGroupSids;
			}
			set
			{
				this.restrictedGroupSids = value;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00016E90 File Offset: 0x00015090
		public override string ToString()
		{
			return this.UserSid;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00016E98 File Offset: 0x00015098
		internal SecurityAccessToken GetSecurityAccessToken()
		{
			SecurityAccessToken securityAccessToken = new SecurityAccessToken();
			securityAccessToken.UserSid = this.UserSid;
			if (this.groupSAArray == null)
			{
				this.groupSAArray = this.SidStringAndAttributeArrayFromGroupSidArray(this.groupSids);
			}
			if (this.restrictedGroupSAArray == null)
			{
				this.restrictedGroupSAArray = this.SidStringAndAttributeArrayFromGroupSidArray(this.restrictedGroupSids);
			}
			securityAccessToken.GroupSids = this.groupSAArray;
			securityAccessToken.RestrictedGroupSids = this.restrictedGroupSAArray;
			return securityAccessToken;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00016F04 File Offset: 0x00015104
		public string GetDebugString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("\nUserSid: {0}", this.UserSid);
			if (this.GroupSids != null)
			{
				stringBuilder.AppendFormat("\nGroup SIDs and Attributes:", new object[0]);
				for (int i = 0; i < this.GroupSids.Length; i++)
				{
					stringBuilder.AppendFormat("\n{0}", this.GroupSids[i]);
				}
			}
			if (this.RestrictedGroupSids != null)
			{
				stringBuilder.AppendFormat("\nRestricted Group SIDs and Attributes:", new object[0]);
				for (int j = 0; j < this.GroupSids.Length; j++)
				{
					stringBuilder.AppendFormat("\n{0}", this.RestrictedGroupSids[j]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016FB4 File Offset: 0x000151B4
		private GroupSid[] GroupSidArrayFromSidStringAndAttributeArray(SidStringAndAttributes[] saArray)
		{
			if (saArray == null || saArray.Length == 0)
			{
				return null;
			}
			GroupSid[] array = new GroupSid[saArray.Length];
			for (int i = 0; i < saArray.Length; i++)
			{
				array[i] = new GroupSid();
				array[i].SecurityIdentifier = saArray[i].SecurityIdentifier;
				array[i].Attributes = saArray[i].Attributes;
			}
			return array;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001700C File Offset: 0x0001520C
		private SidStringAndAttributes[] SidStringAndAttributeArrayFromGroupSidArray(GroupSid[] gsArray)
		{
			if (gsArray == null || gsArray.Length == 0)
			{
				return null;
			}
			SidStringAndAttributes[] array = new SidStringAndAttributes[gsArray.Length];
			for (int i = 0; i < gsArray.Length; i++)
			{
				array[i] = new SidStringAndAttributes(gsArray[i].SecurityIdentifier, gsArray[i].Attributes);
			}
			return array;
		}

		// Token: 0x0400030C RID: 780
		private string userSid;

		// Token: 0x0400030D RID: 781
		private GroupSid[] groupSids;

		// Token: 0x0400030E RID: 782
		private GroupSid[] restrictedGroupSids;

		// Token: 0x0400030F RID: 783
		private SidStringAndAttributes[] groupSAArray;

		// Token: 0x04000310 RID: 784
		private SidStringAndAttributes[] restrictedGroupSAArray;
	}
}
