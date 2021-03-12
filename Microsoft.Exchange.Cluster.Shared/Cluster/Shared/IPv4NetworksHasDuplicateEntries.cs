using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000DD RID: 221
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPv4NetworksHasDuplicateEntries : ClusCommonValidationFailedException
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x0001C554 File Offset: 0x0001A754
		public IPv4NetworksHasDuplicateEntries(string duplicate) : base(Strings.IPv4NetworksHasDuplicateEntries(duplicate))
		{
			this.duplicate = duplicate;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001C56E File Offset: 0x0001A76E
		public IPv4NetworksHasDuplicateEntries(string duplicate, Exception innerException) : base(Strings.IPv4NetworksHasDuplicateEntries(duplicate), innerException)
		{
			this.duplicate = duplicate;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001C589 File Offset: 0x0001A789
		protected IPv4NetworksHasDuplicateEntries(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.duplicate = (string)info.GetValue("duplicate", typeof(string));
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001C5B3 File Offset: 0x0001A7B3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("duplicate", this.duplicate);
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0001C5CE File Offset: 0x0001A7CE
		public string Duplicate
		{
			get
			{
				return this.duplicate;
			}
		}

		// Token: 0x04000736 RID: 1846
		private readonly string duplicate;
	}
}
