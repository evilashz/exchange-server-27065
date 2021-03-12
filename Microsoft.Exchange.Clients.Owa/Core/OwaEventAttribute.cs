using System;
using System.Collections;
using System.Reflection;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000183 RID: 387
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class OwaEventAttribute : Attribute
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x0005B834 File Offset: 0x00059A34
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

		// Token: 0x06000E20 RID: 3616 RVA: 0x0005B871 File Offset: 0x00059A71
		public OwaEventAttribute(string name, bool isInternal) : this(name, isInternal, false)
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0005B87C File Offset: 0x00059A7C
		public OwaEventAttribute(string name) : this(name, false)
		{
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0005B886 File Offset: 0x00059A86
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0005B88E File Offset: 0x00059A8E
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x0005B896 File Offset: 0x00059A96
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

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0005B89F File Offset: 0x00059A9F
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x0005B8A7 File Offset: 0x00059AA7
		internal ulong SegmentationFlags
		{
			get
			{
				return this.segmentationFlags;
			}
			set
			{
				this.segmentationFlags = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0005B8B0 File Offset: 0x00059AB0
		// (set) Token: 0x06000E28 RID: 3624 RVA: 0x0005B8B8 File Offset: 0x00059AB8
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

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0005B8C1 File Offset: 0x00059AC1
		// (set) Token: 0x06000E2A RID: 3626 RVA: 0x0005B8C9 File Offset: 0x00059AC9
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

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0005B8D2 File Offset: 0x00059AD2
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x0005B8DA File Offset: 0x00059ADA
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

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0005B8E3 File Offset: 0x00059AE3
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x0005B8EB File Offset: 0x00059AEB
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0005B8F4 File Offset: 0x00059AF4
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x0005B8FC File Offset: 0x00059AFC
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

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0005B905 File Offset: 0x00059B05
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x0005B90D File Offset: 0x00059B0D
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

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0005B916 File Offset: 0x00059B16
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x0005B91E File Offset: 0x00059B1E
		internal bool AllowAnonymousAccess { get; private set; }

		// Token: 0x06000E35 RID: 3637 RVA: 0x0005B927 File Offset: 0x00059B27
		internal void AddParameterInfo(OwaEventParameterAttribute paramInfo)
		{
			this.paramInfoTable.Add(paramInfo.Name, paramInfo);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0005B93B File Offset: 0x00059B3B
		internal OwaEventParameterAttribute FindParameterInfo(string name)
		{
			return (OwaEventParameterAttribute)this.paramInfoTable[name];
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0005B94E File Offset: 0x00059B4E
		internal Hashtable ParamInfoTable
		{
			get
			{
				return this.paramInfoTable;
			}
		}

		// Token: 0x0400099B RID: 2459
		private string name;

		// Token: 0x0400099C RID: 2460
		private Hashtable paramInfoTable;

		// Token: 0x0400099D RID: 2461
		private MethodInfo methodInfo;

		// Token: 0x0400099E RID: 2462
		private MethodInfo beginMethodInfo;

		// Token: 0x0400099F RID: 2463
		private MethodInfo endMethodInfo;

		// Token: 0x040009A0 RID: 2464
		private ulong requiredMask;

		// Token: 0x040009A1 RID: 2465
		private OwaEventVerb verbs = OwaEventVerb.Post;

		// Token: 0x040009A2 RID: 2466
		private ulong segmentationFlags;

		// Token: 0x040009A3 RID: 2467
		private bool isAsync;

		// Token: 0x040009A4 RID: 2468
		private bool isInternal;
	}
}
