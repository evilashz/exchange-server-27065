using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	// Token: 0x02000311 RID: 785
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		// Token: 0x06002834 RID: 10292 RVA: 0x00094378 File Offset: 0x00092578
		public IdentityNotMappedException() : base(Environment.GetResourceString("IdentityReference_IdentityNotMapped"))
		{
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0009438A File Offset: 0x0009258A
		public IdentityNotMappedException(string message) : base(message)
		{
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x00094393 File Offset: 0x00092593
		public IdentityNotMappedException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0009439D File Offset: 0x0009259D
		internal IdentityNotMappedException(string message, IdentityReferenceCollection unmappedIdentities) : this(message)
		{
			this.unmappedIdentities = unmappedIdentities;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000943AD File Offset: 0x000925AD
		internal IdentityNotMappedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000943B7 File Offset: 0x000925B7
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x000943C1 File Offset: 0x000925C1
		public IdentityReferenceCollection UnmappedIdentities
		{
			get
			{
				if (this.unmappedIdentities == null)
				{
					this.unmappedIdentities = new IdentityReferenceCollection();
				}
				return this.unmappedIdentities;
			}
		}

		// Token: 0x0400104B RID: 4171
		private IdentityReferenceCollection unmappedIdentities;
	}
}
