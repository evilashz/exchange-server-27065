using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public sealed class NonRootLocalLongFullPath : LocalLongFullPath
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003E49 File Offset: 0x00002049
		private NonRootLocalLongFullPath()
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003E51 File Offset: 0x00002051
		public new static NonRootLocalLongFullPath Parse(string pathName)
		{
			return (NonRootLocalLongFullPath)LongPath.ParseInternal(pathName, new NonRootLocalLongFullPath());
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003E63 File Offset: 0x00002063
		protected override bool ParseCore(string path, bool nothrow)
		{
			if (base.ParseCore(path, nothrow) && NonRootLocalLongFullPath.IsRootDirectory(base.PathName))
			{
				base.IsValid = false;
				if (!nothrow)
				{
					throw new ArgumentException(DataStrings.ErrorPathCanNotBeRoot, "path");
				}
			}
			return base.IsValid;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003E9C File Offset: 0x0000209C
		private static bool IsRootDirectory(string path)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(path))
			{
				result = path.Equals(Path.GetPathRoot(path), StringComparison.OrdinalIgnoreCase);
			}
			return result;
		}
	}
}
