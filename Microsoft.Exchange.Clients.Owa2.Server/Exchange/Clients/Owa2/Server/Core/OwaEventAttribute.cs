using System;
using System.Collections;
using System.Reflection;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001D6 RID: 470
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class OwaEventAttribute : Attribute
	{
		// Token: 0x060010A7 RID: 4263 RVA: 0x0003FE17 File Offset: 0x0003E017
		public OwaEventAttribute(string name, bool isInternal, bool allowAnonymousAccess)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.isInternal = isInternal;
			this.AllowAnonymousAccess = allowAnonymousAccess;
			this.paramInfoTable = new Hashtable();
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0003FE54 File Offset: 0x0003E054
		public OwaEventAttribute(string name, bool isInternal) : this(name, isInternal, false)
		{
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0003FE5F File Offset: 0x0003E05F
		public OwaEventAttribute(string name) : this(name, false)
		{
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x0003FE69 File Offset: 0x0003E069
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0003FE71 File Offset: 0x0003E071
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x0003FE79 File Offset: 0x0003E079
		internal bool IsAsync
		{
			get
			{
				return this.isAsync;
			}
			set
			{
				this.isAsync = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0003FE82 File Offset: 0x0003E082
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x0003FE8A File Offset: 0x0003E08A
		internal MethodInfo MethodInfo
		{
			get
			{
				return this.methodInfo;
			}
			set
			{
				this.methodInfo = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x0003FE93 File Offset: 0x0003E093
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x0003FE9B File Offset: 0x0003E09B
		internal MethodInfo BeginMethodInfo
		{
			get
			{
				return this.beginMethodInfo;
			}
			set
			{
				this.beginMethodInfo = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x0003FEAC File Offset: 0x0003E0AC
		internal MethodInfo EndMethodInfo
		{
			get
			{
				return this.endMethodInfo;
			}
			set
			{
				this.endMethodInfo = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0003FEB5 File Offset: 0x0003E0B5
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x0003FEBD File Offset: 0x0003E0BD
		internal ulong RequiredMask
		{
			get
			{
				return this.requiredMask;
			}
			set
			{
				this.requiredMask = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0003FEC6 File Offset: 0x0003E0C6
		// (set) Token: 0x060010B6 RID: 4278 RVA: 0x0003FECE File Offset: 0x0003E0CE
		internal OwaEventVerb AllowedVerbs
		{
			get
			{
				return this.verbs;
			}
			set
			{
				this.verbs = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0003FED7 File Offset: 0x0003E0D7
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x0003FEDF File Offset: 0x0003E0DF
		internal bool IsInternal
		{
			get
			{
				return this.isInternal;
			}
			set
			{
				this.isInternal = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0003FEE8 File Offset: 0x0003E0E8
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x0003FEF0 File Offset: 0x0003E0F0
		internal bool AllowAnonymousAccess { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0003FEF9 File Offset: 0x0003E0F9
		internal Hashtable ParamInfoTable
		{
			get
			{
				return this.paramInfoTable;
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0003FF01 File Offset: 0x0003E101
		internal void AddParameterInfo(OwaEventParameterAttribute paramInfo)
		{
			this.paramInfoTable.Add(paramInfo.Name, paramInfo);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0003FF15 File Offset: 0x0003E115
		internal OwaEventParameterAttribute FindParameterInfo(string name)
		{
			return (OwaEventParameterAttribute)this.paramInfoTable[name];
		}

		// Token: 0x040009D5 RID: 2517
		private string name;

		// Token: 0x040009D6 RID: 2518
		private Hashtable paramInfoTable;

		// Token: 0x040009D7 RID: 2519
		private MethodInfo methodInfo;

		// Token: 0x040009D8 RID: 2520
		private MethodInfo beginMethodInfo;

		// Token: 0x040009D9 RID: 2521
		private MethodInfo endMethodInfo;

		// Token: 0x040009DA RID: 2522
		private ulong requiredMask;

		// Token: 0x040009DB RID: 2523
		private OwaEventVerb verbs = OwaEventVerb.Post;

		// Token: 0x040009DC RID: 2524
		private bool isAsync;

		// Token: 0x040009DD RID: 2525
		private bool isInternal;
	}
}
