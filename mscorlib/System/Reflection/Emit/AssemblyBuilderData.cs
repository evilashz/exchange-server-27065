using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x020005FE RID: 1534
	internal class AssemblyBuilderData
	{
		// Token: 0x0600485D RID: 18525 RVA: 0x001054FC File Offset: 0x001036FC
		[SecurityCritical]
		internal AssemblyBuilderData(InternalAssemblyBuilder assembly, string strAssemblyName, AssemblyBuilderAccess access, string dir)
		{
			this.m_assembly = assembly;
			this.m_strAssemblyName = strAssemblyName;
			this.m_access = access;
			this.m_moduleBuilderList = new List<ModuleBuilder>();
			this.m_resWriterList = new List<ResWriterData>();
			if (dir == null && access != AssemblyBuilderAccess.Run)
			{
				this.m_strDir = Environment.CurrentDirectory;
			}
			else
			{
				this.m_strDir = dir;
			}
			this.m_peFileKind = PEFileKinds.Dll;
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x0010555E File Offset: 0x0010375E
		internal void AddModule(ModuleBuilder dynModule)
		{
			this.m_moduleBuilderList.Add(dynModule);
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x0010556C File Offset: 0x0010376C
		internal void AddResWriter(ResWriterData resData)
		{
			this.m_resWriterList.Add(resData);
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x0010557C File Offset: 0x0010377C
		internal void AddCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (this.m_CABuilders == null)
			{
				this.m_CABuilders = new CustomAttributeBuilder[16];
			}
			if (this.m_iCABuilder == this.m_CABuilders.Length)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.m_iCABuilder * 2];
				Array.Copy(this.m_CABuilders, array, this.m_iCABuilder);
				this.m_CABuilders = array;
			}
			this.m_CABuilders[this.m_iCABuilder] = customBuilder;
			this.m_iCABuilder++;
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x001055F4 File Offset: 0x001037F4
		internal void AddCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (this.m_CABytes == null)
			{
				this.m_CABytes = new byte[16][];
				this.m_CACons = new ConstructorInfo[16];
			}
			if (this.m_iCAs == this.m_CABytes.Length)
			{
				byte[][] array = new byte[this.m_iCAs * 2][];
				ConstructorInfo[] array2 = new ConstructorInfo[this.m_iCAs * 2];
				for (int i = 0; i < this.m_iCAs; i++)
				{
					array[i] = this.m_CABytes[i];
					array2[i] = this.m_CACons[i];
				}
				this.m_CABytes = array;
				this.m_CACons = array2;
			}
			byte[] array3 = new byte[binaryAttribute.Length];
			Array.Copy(binaryAttribute, array3, binaryAttribute.Length);
			this.m_CABytes[this.m_iCAs] = array3;
			this.m_CACons[this.m_iCAs] = con;
			this.m_iCAs++;
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x001056C4 File Offset: 0x001038C4
		[SecurityCritical]
		internal void FillUnmanagedVersionInfo()
		{
			CultureInfo locale = this.m_assembly.GetLocale();
			if (locale != null)
			{
				this.m_nativeVersion.m_lcid = locale.LCID;
			}
			for (int i = 0; i < this.m_iCABuilder; i++)
			{
				Type declaringType = this.m_CABuilders[i].m_con.DeclaringType;
				if (this.m_CABuilders[i].m_constructorArgs.Length != 0 && this.m_CABuilders[i].m_constructorArgs[0] != null)
				{
					if (declaringType.Equals(typeof(AssemblyCopyrightAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[]
							{
								this.m_CABuilders[i].m_con.ReflectedType.Name
							}));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strCopyright = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyTrademarkAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[]
							{
								this.m_CABuilders[i].m_con.ReflectedType.Name
							}));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strTrademark = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyProductAttribute)))
					{
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strProduct = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyCompanyAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[]
							{
								this.m_CABuilders[i].m_con.ReflectedType.Name
							}));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strCompany = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyDescriptionAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[]
							{
								this.m_CABuilders[i].m_con.ReflectedType.Name
							}));
						}
						this.m_nativeVersion.m_strDescription = this.m_CABuilders[i].m_constructorArgs[0].ToString();
					}
					else if (declaringType.Equals(typeof(AssemblyTitleAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[]
							{
								this.m_CABuilders[i].m_con.ReflectedType.Name
							}));
						}
						this.m_nativeVersion.m_strTitle = this.m_CABuilders[i].m_constructorArgs[0].ToString();
					}
					else if (declaringType.Equals(typeof(AssemblyInformationalVersionAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[]
							{
								this.m_CABuilders[i].m_con.ReflectedType.Name
							}));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strProductVersion = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyCultureAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[]
							{
								this.m_CABuilders[i].m_con.ReflectedType.Name
							}));
						}
						CultureInfo cultureInfo = new CultureInfo(this.m_CABuilders[i].m_constructorArgs[0].ToString());
						this.m_nativeVersion.m_lcid = cultureInfo.LCID;
					}
					else if (declaringType.Equals(typeof(AssemblyFileVersionAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[]
							{
								this.m_CABuilders[i].m_con.ReflectedType.Name
							}));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strFileVersion = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
				}
			}
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x00105B7C File Offset: 0x00103D7C
		internal void CheckResNameConflict(string strNewResName)
		{
			int count = this.m_resWriterList.Count;
			for (int i = 0; i < count; i++)
			{
				ResWriterData resWriterData = this.m_resWriterList[i];
				if (resWriterData.m_strName.Equals(strNewResName))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateResourceName"));
				}
			}
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x00105BCC File Offset: 0x00103DCC
		internal void CheckNameConflict(string strNewModuleName)
		{
			int count = this.m_moduleBuilderList.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strModuleName.Equals(strNewModuleName))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateModuleName"));
				}
			}
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x00105C24 File Offset: 0x00103E24
		internal void CheckTypeNameConflict(string strTypeName, TypeBuilder enclosingType)
		{
			for (int i = 0; i < this.m_moduleBuilderList.Count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				moduleBuilder.CheckTypeNameConflict(strTypeName, enclosingType);
			}
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x00105C5C File Offset: 0x00103E5C
		internal void CheckFileNameConflict(string strFileName)
		{
			int count = this.m_moduleBuilderList.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strFileName != null && string.Compare(moduleBuilder.m_moduleData.m_strFileName, strFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DuplicatedFileName"));
				}
			}
			count = this.m_resWriterList.Count;
			for (int i = 0; i < count; i++)
			{
				ResWriterData resWriterData = this.m_resWriterList[i];
				if (resWriterData.m_strFileName != null && string.Compare(resWriterData.m_strFileName, strFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DuplicatedFileName"));
				}
			}
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x00105D0C File Offset: 0x00103F0C
		internal ModuleBuilder FindModuleWithFileName(string strFileName)
		{
			int count = this.m_moduleBuilderList.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strFileName != null && string.Compare(moduleBuilder.m_moduleData.m_strFileName, strFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return moduleBuilder;
				}
			}
			return null;
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x00105D64 File Offset: 0x00103F64
		internal ModuleBuilder FindModuleWithName(string strName)
		{
			int count = this.m_moduleBuilderList.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strModuleName != null && string.Compare(moduleBuilder.m_moduleData.m_strModuleName, strName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return moduleBuilder;
				}
			}
			return null;
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x00105DBA File Offset: 0x00103FBA
		internal void AddPublicComType(Type type)
		{
			if (this.m_isSaved)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotAlterAssembly"));
			}
			this.EnsurePublicComTypeCapacity();
			this.m_publicComTypeList[this.m_iPublicComTypeCount] = type;
			this.m_iPublicComTypeCount++;
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x00105DF6 File Offset: 0x00103FF6
		internal void AddPermissionRequests(PermissionSet required, PermissionSet optional, PermissionSet refused)
		{
			if (this.m_isSaved)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotAlterAssembly"));
			}
			this.m_RequiredPset = required;
			this.m_OptionalPset = optional;
			this.m_RefusedPset = refused;
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x00105E28 File Offset: 0x00104028
		private void EnsurePublicComTypeCapacity()
		{
			if (this.m_publicComTypeList == null)
			{
				this.m_publicComTypeList = new Type[16];
			}
			if (this.m_iPublicComTypeCount == this.m_publicComTypeList.Length)
			{
				Type[] array = new Type[this.m_iPublicComTypeCount * 2];
				Array.Copy(this.m_publicComTypeList, array, this.m_iPublicComTypeCount);
				this.m_publicComTypeList = array;
			}
		}

		// Token: 0x04001D95 RID: 7573
		internal List<ModuleBuilder> m_moduleBuilderList;

		// Token: 0x04001D96 RID: 7574
		internal List<ResWriterData> m_resWriterList;

		// Token: 0x04001D97 RID: 7575
		internal string m_strAssemblyName;

		// Token: 0x04001D98 RID: 7576
		internal AssemblyBuilderAccess m_access;

		// Token: 0x04001D99 RID: 7577
		private InternalAssemblyBuilder m_assembly;

		// Token: 0x04001D9A RID: 7578
		internal Type[] m_publicComTypeList;

		// Token: 0x04001D9B RID: 7579
		internal int m_iPublicComTypeCount;

		// Token: 0x04001D9C RID: 7580
		internal bool m_isSaved;

		// Token: 0x04001D9D RID: 7581
		internal const int m_iInitialSize = 16;

		// Token: 0x04001D9E RID: 7582
		internal string m_strDir;

		// Token: 0x04001D9F RID: 7583
		internal const int m_tkAssembly = 536870913;

		// Token: 0x04001DA0 RID: 7584
		internal PermissionSet m_RequiredPset;

		// Token: 0x04001DA1 RID: 7585
		internal PermissionSet m_OptionalPset;

		// Token: 0x04001DA2 RID: 7586
		internal PermissionSet m_RefusedPset;

		// Token: 0x04001DA3 RID: 7587
		internal CustomAttributeBuilder[] m_CABuilders;

		// Token: 0x04001DA4 RID: 7588
		internal int m_iCABuilder;

		// Token: 0x04001DA5 RID: 7589
		internal byte[][] m_CABytes;

		// Token: 0x04001DA6 RID: 7590
		internal ConstructorInfo[] m_CACons;

		// Token: 0x04001DA7 RID: 7591
		internal int m_iCAs;

		// Token: 0x04001DA8 RID: 7592
		internal PEFileKinds m_peFileKind;

		// Token: 0x04001DA9 RID: 7593
		internal MethodInfo m_entryPointMethod;

		// Token: 0x04001DAA RID: 7594
		internal Assembly m_ISymWrapperAssembly;

		// Token: 0x04001DAB RID: 7595
		internal ModuleBuilder m_entryPointModule;

		// Token: 0x04001DAC RID: 7596
		internal string m_strResourceFileName;

		// Token: 0x04001DAD RID: 7597
		internal byte[] m_resourceBytes;

		// Token: 0x04001DAE RID: 7598
		internal NativeVersionInfo m_nativeVersion;

		// Token: 0x04001DAF RID: 7599
		internal bool m_hasUnmanagedVersionInfo;

		// Token: 0x04001DB0 RID: 7600
		internal bool m_OverrideUnmanagedVersionInfo;
	}
}
