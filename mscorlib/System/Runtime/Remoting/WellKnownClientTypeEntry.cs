using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000796 RID: 1942
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		// Token: 0x060054E3 RID: 21731 RVA: 0x0012C728 File Offset: 0x0012A928
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._objectUrl = objectUrl;
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x0012C77C File Offset: 0x0012A97C
		public WellKnownClientTypeEntry(Type type, string objectUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
			this._objectUrl = objectUrl;
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x060054E5 RID: 21733 RVA: 0x0012C7F5 File Offset: 0x0012A9F5
		public string ObjectUrl
		{
			get
			{
				return this._objectUrl;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x0012C800 File Offset: 0x0012AA00
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x060054E7 RID: 21735 RVA: 0x0012C82C File Offset: 0x0012AA2C
		// (set) Token: 0x060054E8 RID: 21736 RVA: 0x0012C834 File Offset: 0x0012AA34
		public string ApplicationUrl
		{
			get
			{
				return this._appUrl;
			}
			set
			{
				this._appUrl = value;
			}
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0012C840 File Offset: 0x0012AA40
		public override string ToString()
		{
			string text = string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'; url=",
				this._objectUrl
			});
			if (this._appUrl != null)
			{
				text = text + "; appUrl=" + this._appUrl;
			}
			return text;
		}

		// Token: 0x040026C8 RID: 9928
		private string _objectUrl;

		// Token: 0x040026C9 RID: 9929
		private string _appUrl;
	}
}
