using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x02000468 RID: 1128
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class OwaHelpUrlData : SerializableDataBase, IEquatable<OwaHelpUrlData>
	{
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000CDDA5 File Offset: 0x000CBFA5
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000CDDAD File Offset: 0x000CBFAD
		[DataMember]
		public string HelpUrl { get; set; }

		// Token: 0x06003287 RID: 12935 RVA: 0x000CDDB6 File Offset: 0x000CBFB6
		public bool Equals(OwaHelpUrlData other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(other, this) || string.Equals(this.HelpUrl, other.HelpUrl, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000CDDE0 File Offset: 0x000CBFE0
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as OwaHelpUrlData);
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000CDDF0 File Offset: 0x000CBFF0
		protected override int InternalGetHashCode()
		{
			int num = 17;
			return num * 397 ^ (this.HelpUrl ?? string.Empty).ToLowerInvariant().GetHashCode();
		}
	}
}
