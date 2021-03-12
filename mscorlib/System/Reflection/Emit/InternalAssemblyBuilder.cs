using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020005FC RID: 1532
	internal sealed class InternalAssemblyBuilder : RuntimeAssembly
	{
		// Token: 0x060047E5 RID: 18405 RVA: 0x00103427 File Offset: 0x00101627
		private InternalAssemblyBuilder()
		{
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x0010342F File Offset: 0x0010162F
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InternalAssemblyBuilder)
			{
				return this == obj;
			}
			return obj.Equals(this);
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x0010344A File Offset: 0x0010164A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x00103452 File Offset: 0x00101652
		public override string[] GetManifestResourceNames()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x00103463 File Offset: 0x00101663
		public override FileStream GetFile(string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x00103474 File Offset: 0x00101674
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x00103485 File Offset: 0x00101685
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x00103496 File Offset: 0x00101696
		public override Stream GetManifestResourceStream(string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x001034A7 File Offset: 0x001016A7
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x001034B8 File Offset: 0x001016B8
		public override string Location
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x001034C9 File Offset: 0x001016C9
		public override string CodeBase
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
			}
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x001034DA File Offset: 0x001016DA
		public override Type[] GetExportedTypes()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x001034EB File Offset: 0x001016EB
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeEnvironment.GetSystemVersion();
			}
		}
	}
}
