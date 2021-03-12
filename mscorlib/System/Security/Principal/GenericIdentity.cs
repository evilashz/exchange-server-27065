using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
	// Token: 0x020002F5 RID: 757
	[ComVisible(true)]
	[Serializable]
	public class GenericIdentity : ClaimsIdentity
	{
		// Token: 0x0600276A RID: 10090 RVA: 0x0009018D File Offset: 0x0008E38D
		[SecuritySafeCritical]
		public GenericIdentity(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = name;
			this.m_type = "";
			this.AddNameClaim();
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000901BB File Offset: 0x0008E3BB
		[SecuritySafeCritical]
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
			this.AddNameClaim();
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000901F3 File Offset: 0x0008E3F3
		private GenericIdentity()
		{
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000901FB File Offset: 0x0008E3FB
		protected GenericIdentity(GenericIdentity identity) : base(identity)
		{
			this.m_name = identity.m_name;
			this.m_type = identity.m_type;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0009021C File Offset: 0x0008E41C
		public override ClaimsIdentity Clone()
		{
			return new GenericIdentity(this);
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x00090224 File Offset: 0x0008E424
		public override IEnumerable<Claim> Claims
		{
			get
			{
				return base.Claims;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x0009022C File Offset: 0x0008E42C
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x00090234 File Offset: 0x0008E434
		public override string AuthenticationType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x0009023C File Offset: 0x0008E43C
		public override bool IsAuthenticated
		{
			get
			{
				return !this.m_name.Equals("");
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00090254 File Offset: 0x0008E454
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			bool flag = false;
			using (IEnumerator<Claim> enumerator = base.Claims.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Claim claim = enumerator.Current;
					flag = true;
				}
			}
			if (!flag)
			{
				this.AddNameClaim();
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000902AC File Offset: 0x0008E4AC
		[SecuritySafeCritical]
		private void AddNameClaim()
		{
			if (this.m_name != null)
			{
				base.AddClaim(new Claim(base.NameClaimType, this.m_name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
		}

		// Token: 0x04000F4F RID: 3919
		private string m_name;

		// Token: 0x04000F50 RID: 3920
		private string m_type;
	}
}
