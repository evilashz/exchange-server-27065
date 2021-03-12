using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000797 RID: 1943
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		// Token: 0x060054EA RID: 21738 RVA: 0x0012C8A8 File Offset: 0x0012AAA8
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (objectUri == null)
			{
				throw new ArgumentNullException("objectUri");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._objectUri = objectUri;
			this._mode = mode;
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x0012C904 File Offset: 0x0012AB04
		public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUri == null)
			{
				throw new ArgumentNullException("objectUri");
			}
			if (!(type is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = type.Module.Assembly.FullName;
			this._objectUri = objectUri;
			this._mode = mode;
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060054EC RID: 21740 RVA: 0x0012C981 File Offset: 0x0012AB81
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x0012C989 File Offset: 0x0012AB89
		public WellKnownObjectMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060054EE RID: 21742 RVA: 0x0012C994 File Offset: 0x0012AB94
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060054EF RID: 21743 RVA: 0x0012C9C0 File Offset: 0x0012ABC0
		// (set) Token: 0x060054F0 RID: 21744 RVA: 0x0012C9C8 File Offset: 0x0012ABC8
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return this._contextAttributes;
			}
			set
			{
				this._contextAttributes = value;
			}
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0012C9D4 File Offset: 0x0012ABD4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'; objectUri=",
				this._objectUri,
				"; mode=",
				this._mode.ToString()
			});
		}

		// Token: 0x040026CA RID: 9930
		private string _objectUri;

		// Token: 0x040026CB RID: 9931
		private WellKnownObjectMode _mode;

		// Token: 0x040026CC RID: 9932
		private IContextAttribute[] _contextAttributes;
	}
}
