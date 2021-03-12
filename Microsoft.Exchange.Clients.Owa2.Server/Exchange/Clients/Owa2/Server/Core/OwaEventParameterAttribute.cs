using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001DD RID: 477
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class OwaEventParameterAttribute : Attribute
	{
		// Token: 0x060010E3 RID: 4323 RVA: 0x00040508 File Offset: 0x0003E708
		public OwaEventParameterAttribute(string name, Type type, bool isArray, bool isOptional)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.name = name;
			this.isOptional = isOptional;
			this.isArray = isArray;
			this.paramType = type;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0004055A File Offset: 0x0003E75A
		public OwaEventParameterAttribute(string name, Type type, bool isArray) : this(name, type, isArray, false)
		{
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00040566 File Offset: 0x0003E766
		public OwaEventParameterAttribute(string name, Type type) : this(name, type, false, false)
		{
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00040572 File Offset: 0x0003E772
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0004057A File Offset: 0x0003E77A
		internal Type Type
		{
			get
			{
				return this.paramType;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00040582 File Offset: 0x0003E782
		internal bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0004058A File Offset: 0x0003E78A
		internal bool IsArray
		{
			get
			{
				return this.isArray;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00040592 File Offset: 0x0003E792
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x0004059A File Offset: 0x0003E79A
		internal ulong ParameterMask
		{
			get
			{
				return this.parameterMask;
			}
			set
			{
				this.parameterMask = value;
			}
		}

		// Token: 0x040009F9 RID: 2553
		private string name;

		// Token: 0x040009FA RID: 2554
		private Type paramType;

		// Token: 0x040009FB RID: 2555
		private bool isOptional;

		// Token: 0x040009FC RID: 2556
		private bool isArray;

		// Token: 0x040009FD RID: 2557
		private ulong parameterMask;
	}
}
