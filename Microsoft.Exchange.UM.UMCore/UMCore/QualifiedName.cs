using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001BB RID: 443
	internal class QualifiedName
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x00038A8F File Offset: 0x00036C8F
		internal QualifiedName(string rawName, ActivityManagerConfig defaultNamespace) : this(rawName, (defaultNamespace == null) ? "GlobalActivityManager" : defaultNamespace.ClassName)
		{
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00038AA8 File Offset: 0x00036CA8
		internal QualifiedName(string rawName, string defaultNamespace)
		{
			if (-1 != rawName.IndexOf(":", StringComparison.InvariantCulture))
			{
				string[] array = rawName.Split(new char[]
				{
					':'
				});
				if (array == null || 2 != array.Length)
				{
					throw new FsmConfigurationException(Strings.InvalidQualifiedName(rawName));
				}
				this.nameSpace = array[0];
				this.shortName = array[1];
			}
			else
			{
				this.shortName = rawName;
				this.nameSpace = defaultNamespace;
			}
			this.fullName = defaultNamespace + ":" + this.shortName;
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00038B31 File Offset: 0x00036D31
		internal string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00038B39 File Offset: 0x00036D39
		internal string ShortName
		{
			get
			{
				return this.shortName;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00038B41 File Offset: 0x00036D41
		internal string Namespace
		{
			get
			{
				return this.nameSpace;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00038B49 File Offset: 0x00036D49
		public override string ToString()
		{
			return this.fullName;
		}

		// Token: 0x04000A61 RID: 2657
		private string nameSpace;

		// Token: 0x04000A62 RID: 2658
		private string shortName;

		// Token: 0x04000A63 RID: 2659
		private string fullName;
	}
}
