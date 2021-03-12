using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F0 RID: 2544
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class Identity : INamedIdentity
	{
		// Token: 0x060047DF RID: 18399 RVA: 0x00100B59 File Offset: 0x000FED59
		public Identity(string identity) : this(identity, identity)
		{
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x00100B63 File Offset: 0x000FED63
		public Identity(ADObjectId identity) : this(identity, identity.Name)
		{
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x00100B74 File Offset: 0x000FED74
		internal Identity(string rawIdentity, string displayName)
		{
			this.RawIdentity = rawIdentity;
			this.DisplayName = displayName;
			this.OnDeserialized(default(StreamingContext));
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x00100BA4 File Offset: 0x000FEDA4
		internal Identity(ADObjectId identity, string displayName) : this((identity.ObjectGuid == Guid.Empty) ? identity.DistinguishedName : identity.ObjectGuid.ToString(), displayName)
		{
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x00100BE6 File Offset: 0x000FEDE6
		// (set) Token: 0x060047E4 RID: 18404 RVA: 0x00100BEE File Offset: 0x000FEDEE
		[DataMember]
		public string DisplayName { get; private set; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x060047E5 RID: 18405 RVA: 0x00100BF7 File Offset: 0x000FEDF7
		// (set) Token: 0x060047E6 RID: 18406 RVA: 0x00100BFF File Offset: 0x000FEDFF
		[DataMember(IsRequired = true)]
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
			private set
			{
				ServiceCommandBase.ThrowIfNull(value, "value", "Identity:RawIdentity:Set");
				this.rawIdentity = value;
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x00100C18 File Offset: 0x000FEE18
		string INamedIdentity.Identity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x00100C20 File Offset: 0x000FEE20
		public static bool operator ==(Identity identity1, Identity identity2)
		{
			return (identity1 == null && identity2 == null) || (identity1 != null && identity2 != null && string.Compare(identity1.RawIdentity, identity2.RawIdentity, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x00100C58 File Offset: 0x000FEE58
		public static bool operator !=(Identity identity1, Identity identity2)
		{
			return !(identity1 == identity2);
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x00100C64 File Offset: 0x000FEE64
		public bool Equals(Identity identity)
		{
			return this == identity;
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x00100C6D File Offset: 0x000FEE6D
		public override bool Equals(object obj)
		{
			return this.Equals((Identity)obj);
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x00100C7B File Offset: 0x000FEE7B
		public override int GetHashCode()
		{
			return this.RawIdentity.GetHashCode();
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x00100C88 File Offset: 0x000FEE88
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (string.IsNullOrEmpty(this.DisplayName))
			{
				this.DisplayName = this.RawIdentity;
			}
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x00100CA4 File Offset: 0x000FEEA4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{",
				this.RawIdentity,
				",",
				this.DisplayName,
				"}"
			});
		}

		// Token: 0x0400292D RID: 10541
		private string rawIdentity;
	}
}
