using System;
using System.IO;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000625 RID: 1573
	[Serializable]
	internal class ModuleBuilderData
	{
		// Token: 0x06004B7E RID: 19326 RVA: 0x00111007 File Offset: 0x0010F207
		[SecurityCritical]
		internal ModuleBuilderData(ModuleBuilder module, string strModuleName, string strFileName, int tkFile)
		{
			this.m_globalTypeBuilder = new TypeBuilder(module);
			this.m_module = module;
			this.m_tkFile = tkFile;
			this.InitNames(strModuleName, strFileName);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x00111034 File Offset: 0x0010F234
		[SecurityCritical]
		private void InitNames(string strModuleName, string strFileName)
		{
			this.m_strModuleName = strModuleName;
			if (strFileName == null)
			{
				this.m_strFileName = strModuleName;
				return;
			}
			string extension = Path.GetExtension(strFileName);
			if (extension == null || extension == string.Empty)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoModuleFileExtension", new object[]
				{
					strFileName
				}));
			}
			this.m_strFileName = strFileName;
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x0011108B File Offset: 0x0010F28B
		[SecurityCritical]
		internal virtual void ModifyModuleName(string strModuleName)
		{
			this.InitNames(strModuleName, null);
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004B81 RID: 19329 RVA: 0x00111095 File Offset: 0x0010F295
		// (set) Token: 0x06004B82 RID: 19330 RVA: 0x0011109D File Offset: 0x0010F29D
		internal int FileToken
		{
			get
			{
				return this.m_tkFile;
			}
			set
			{
				this.m_tkFile = value;
			}
		}

		// Token: 0x04001EA0 RID: 7840
		internal string m_strModuleName;

		// Token: 0x04001EA1 RID: 7841
		internal string m_strFileName;

		// Token: 0x04001EA2 RID: 7842
		internal bool m_fGlobalBeenCreated;

		// Token: 0x04001EA3 RID: 7843
		internal bool m_fHasGlobal;

		// Token: 0x04001EA4 RID: 7844
		[NonSerialized]
		internal TypeBuilder m_globalTypeBuilder;

		// Token: 0x04001EA5 RID: 7845
		[NonSerialized]
		internal ModuleBuilder m_module;

		// Token: 0x04001EA6 RID: 7846
		private int m_tkFile;

		// Token: 0x04001EA7 RID: 7847
		internal bool m_isSaved;

		// Token: 0x04001EA8 RID: 7848
		[NonSerialized]
		internal ResWriterData m_embeddedRes;

		// Token: 0x04001EA9 RID: 7849
		internal const string MULTI_BYTE_VALUE_CLASS = "$ArrayType$";

		// Token: 0x04001EAA RID: 7850
		internal string m_strResourceFileName;

		// Token: 0x04001EAB RID: 7851
		internal byte[] m_resourceBytes;
	}
}
