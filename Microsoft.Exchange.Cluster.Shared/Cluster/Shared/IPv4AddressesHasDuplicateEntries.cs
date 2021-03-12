using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000DC RID: 220
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPv4AddressesHasDuplicateEntries : ClusCommonValidationFailedException
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0001C4D2 File Offset: 0x0001A6D2
		public IPv4AddressesHasDuplicateEntries(string duplicate) : base(Strings.IPv4AddressesHasDuplicateEntries(duplicate))
		{
			this.duplicate = duplicate;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		public IPv4AddressesHasDuplicateEntries(string duplicate, Exception innerException) : base(Strings.IPv4AddressesHasDuplicateEntries(duplicate), innerException)
		{
			this.duplicate = duplicate;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001C507 File Offset: 0x0001A707
		protected IPv4AddressesHasDuplicateEntries(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.duplicate = (string)info.GetValue("duplicate", typeof(string));
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001C531 File Offset: 0x0001A731
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("duplicate", this.duplicate);
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001C54C File Offset: 0x0001A74C
		public string Duplicate
		{
			get
			{
				return this.duplicate;
			}
		}

		// Token: 0x04000735 RID: 1845
		private readonly string duplicate;
	}
}
