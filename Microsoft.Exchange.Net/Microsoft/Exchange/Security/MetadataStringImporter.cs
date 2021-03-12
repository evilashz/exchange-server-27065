using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A3F RID: 2623
	internal static class MetadataStringImporter
	{
		// Token: 0x06003910 RID: 14608 RVA: 0x00091770 File Offset: 0x0008F970
		public static uint GetUserStrings(string assemblyPath, HashSet<string> results)
		{
			if (assemblyPath == null)
			{
				throw new ArgumentNullException("assemblyPath");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			uint num = 0U;
			uint num2 = 0U;
			uint[] array = new uint[256];
			char[] array2 = new char[256];
			object obj = null;
			MetadataStringImporter.IMetaDataImport metaDataImport = null;
			Guid guid = new Guid("7DAC8207-D3AE-4C75-9B67-92801A497D44");
			MetadataStringImporter.IMetaDataDispenserEx metaDataDispenserEx = (MetadataStringImporter.MetaDataDispenserEx)new MetadataStringImporter.CorMetaDataDispenserExClass();
			try
			{
				uint num3 = metaDataDispenserEx.OpenScope(assemblyPath, 16U, ref guid, out obj);
				if (num3 >= 2147483648U)
				{
					return num3;
				}
				metaDataImport = (MetadataStringImporter.IMetaDataImport)obj;
				while ((num3 = metaDataImport.EnumUserStrings(ref num, array, (uint)(array.Length * Marshal.SizeOf(typeof(uint))), out num2)) < 2147483648U && num2 > 0U)
				{
					uint num4 = 0U;
					int num5 = 0;
					while ((long)num5 < (long)((ulong)num2))
					{
						bool flag;
						do
						{
							flag = false;
							num3 = metaDataImport.GetUserString(array[num5], array2, (uint)array2.Length, out num4);
							if (num3 >= 2147483648U)
							{
								goto Block_6;
							}
							if ((ulong)num4 > (ulong)((long)array2.Length))
							{
								array2 = new char[num4 * 2U];
								flag = true;
							}
						}
						while (flag);
						string text = new string(array2, 0, (int)num4);
						text = string.Intern(text);
						if (!results.Contains(text))
						{
							results.Add(text);
						}
						num5++;
						continue;
						Block_6:
						return num3;
					}
				}
				if (num3 >= 2147483648U)
				{
					return num3;
				}
			}
			finally
			{
				if (metaDataImport != null)
				{
					Marshal.ReleaseComObject(metaDataImport);
				}
				if (metaDataDispenserEx != null)
				{
					Marshal.ReleaseComObject(metaDataDispenserEx);
				}
			}
			return 0U;
		}

		// Token: 0x040030E2 RID: 12514
		private const string MetadataImportIID = "7DAC8207-D3AE-4C75-9B67-92801A497D44";

		// Token: 0x02000A40 RID: 2624
		[Guid("31BCFCE2-DAFB-11D2-9F81-00C04F79A0A3")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IMetaDataDispenserEx
		{
			// Token: 0x06003911 RID: 14609
			uint DefineScope(ref Guid rclsid, uint dwCreateFlags, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppIUnk);

			// Token: 0x06003912 RID: 14610
			uint OpenScope([MarshalAs(UnmanagedType.LPWStr)] string szScope, uint dwOpenFlags, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppIUnk);

			// Token: 0x06003913 RID: 14611
			uint OpenScopeOnMemory(IntPtr pData, uint cbData, uint dwOpenFlags, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppIUnk);

			// Token: 0x06003914 RID: 14612
			uint SetOption(ref Guid optionid, [MarshalAs(UnmanagedType.Struct)] object value);

			// Token: 0x06003915 RID: 14613
			uint GetOption(ref Guid optionid, [MarshalAs(UnmanagedType.Struct)] out object pvalue);

			// Token: 0x06003916 RID: 14614
			uint OpenScopeOnITypeInfo([MarshalAs(UnmanagedType.Interface)] ITypeInfo pITI, uint dwOpenFlags, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppIUnk);

			// Token: 0x06003917 RID: 14615
			uint GetCORSystemDirectory([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] char[] szBuffer, uint cchBuffer, out uint pchBuffer);

			// Token: 0x06003918 RID: 14616
			uint FindAssembly([MarshalAs(UnmanagedType.LPWStr)] string szAppBase, [MarshalAs(UnmanagedType.LPWStr)] string szPrivateBin, [MarshalAs(UnmanagedType.LPWStr)] string szGlobalBin, [MarshalAs(UnmanagedType.LPWStr)] string szAssemblyName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] char[] szName, uint cchName, out uint pcName);

			// Token: 0x06003919 RID: 14617
			uint FindAssemblyModule([MarshalAs(UnmanagedType.LPWStr)] string szAppBase, [MarshalAs(UnmanagedType.LPWStr)] string szPrivateBin, [MarshalAs(UnmanagedType.LPWStr)] string szGlobalBin, [MarshalAs(UnmanagedType.LPWStr)] string szAssemblyName, [MarshalAs(UnmanagedType.LPWStr)] string szModuleName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] char[] szName, uint cchName, out uint pcName);
		}

		// Token: 0x02000A41 RID: 2625
		[Guid("E5CB7A31-7512-11D2-89CE-0080C792E5D8")]
		[ComImport]
		private class CorMetaDataDispenserExClass
		{
			// Token: 0x0600391A RID: 14618
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern CorMetaDataDispenserExClass();
		}

		// Token: 0x02000A42 RID: 2626
		[Guid("31BCFCE2-DAFB-11D2-9F81-00C04F79A0A3")]
		[CoClass(typeof(MetadataStringImporter.CorMetaDataDispenserExClass))]
		[ComImport]
		private interface MetaDataDispenserEx : MetadataStringImporter.IMetaDataDispenserEx
		{
		}

		// Token: 0x02000A43 RID: 2627
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("7DAC8207-D3AE-4C75-9B67-92801A497D44")]
		[ComImport]
		private interface IMetaDataImport
		{
			// Token: 0x0600391B RID: 14619
			void CloseEnum(uint hEnum);

			// Token: 0x0600391C RID: 14620
			uint CountEnum(uint hEnum, out uint count);

			// Token: 0x0600391D RID: 14621
			uint ResetEnum(uint hEnum, uint ulPos);

			// Token: 0x0600391E RID: 14622
			uint EnumTypeDefs(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rTypeDefs, uint cMax, out uint pcTypeDefs);

			// Token: 0x0600391F RID: 14623
			uint EnumInterfaceImpls(ref uint phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rImpls, uint cMax, out uint pcImpls);

			// Token: 0x06003920 RID: 14624
			uint EnumTypeRefs(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rTypeDefs, uint cMax, out uint pcTypeRefs);

			// Token: 0x06003921 RID: 14625
			uint FindTypeDefByName([MarshalAs(UnmanagedType.LPWStr)] string szTypeDef, uint tkEnclosingClass, out uint ptd);

			// Token: 0x06003922 RID: 14626
			uint GetScopeProps([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] char[] szName, uint cchName, out uint pchName, ref Guid pmvid);

			// Token: 0x06003923 RID: 14627
			uint GetModuleFromScope(out uint pmd);

			// Token: 0x06003924 RID: 14628
			uint GetTypeDefProps(uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] szTypeDef, uint cchTypeDef, out uint pchTypeDef, out uint pdwTypeDefFlags, out uint ptkExtends);

			// Token: 0x06003925 RID: 14629
			uint GetInterfaceImplProps(uint iiImpl, out uint pClass, out uint ptkIface);

			// Token: 0x06003926 RID: 14630
			uint GetTypeRefProps(uint tr, out uint ptkResolutionScope, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] char[] szName, uint cchName, out uint pchName);

			// Token: 0x06003927 RID: 14631
			uint ResolveTypeRef(uint tr, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppIScope, out uint ptd);

			// Token: 0x06003928 RID: 14632
			uint EnumMembers(ref uint phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rMembers, uint cMax, out uint pcTokens);

			// Token: 0x06003929 RID: 14633
			uint EnumMembersWithName(ref uint phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)] string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rMembers, uint cMax, out uint pcTokens);

			// Token: 0x0600392A RID: 14634
			uint EnumMethods(ref uint phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rMethods, uint cMax, out uint pcTokens);

			// Token: 0x0600392B RID: 14635
			uint EnumMethodsWithName(ref uint phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)] string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rMethods, uint cMax, out uint pcTokens);

			// Token: 0x0600392C RID: 14636
			uint EnumFields(ref uint phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rFields, uint cMax, out uint pcTokens);

			// Token: 0x0600392D RID: 14637
			uint EnumFieldsWithName(ref uint phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)] string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] rFields, uint cMax, out uint pcTokens);

			// Token: 0x0600392E RID: 14638
			uint EnumParams(ref uint phEnum, uint mb, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rParams, uint cMax, out uint pcTokens);

			// Token: 0x0600392F RID: 14639
			uint EnumMemberRefs(ref uint phEnum, uint tkParent, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rMemberRefs, uint cMax, out uint pcTokens);

			// Token: 0x06003930 RID: 14640
			uint EnumMethodImpls(ref uint phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rMethodBody, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rMethodDecl, uint cMax, out uint pcTokens);

			// Token: 0x06003931 RID: 14641
			uint EnumPermissionSets(ref uint phEnum, uint tk, uint dwActions, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rPermission, uint cMax, out uint pcTokens);

			// Token: 0x06003932 RID: 14642
			uint FindMember(uint td, [MarshalAs(UnmanagedType.LPWStr)] string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

			// Token: 0x06003933 RID: 14643
			uint FindMethod(uint td, [MarshalAs(UnmanagedType.LPWStr)] string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

			// Token: 0x06003934 RID: 14644
			uint FindField(uint td, [MarshalAs(UnmanagedType.LPWStr)] string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

			// Token: 0x06003935 RID: 14645
			uint FindMemberRef(uint td, [MarshalAs(UnmanagedType.LPWStr)] string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pvSigBlob, int cbSigBlob, out uint pmr);

			// Token: 0x06003936 RID: 14646
			uint GetMethodProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] szMethod, uint cchMethod, out uint pchMethod, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags);

			// Token: 0x06003937 RID: 14647
			uint GetMemberRefProps(uint mr, out uint ptk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] szMember, uint cchMember, out uint pchMember, out IntPtr ppvSigBlob, out uint pbSigBlob);

			// Token: 0x06003938 RID: 14648
			uint EnumProperties(ref uint phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rProperties, uint cMax, out uint pcProperties);

			// Token: 0x06003939 RID: 14649
			uint EnumEvents(ref uint phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rEvents, uint cMax, out uint pcEvents);

			// Token: 0x0600393A RID: 14650
			uint GetEventProps(uint ev, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] szEvent, uint cchEvent, out uint pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 10)] uint[] rmdOtherMethod, uint cMax, out uint pcOtherMethod);

			// Token: 0x0600393B RID: 14651
			uint EnumMethodSemantics(ref uint phEnum, uint mb, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rEventProp, uint cMax, out uint pcEventProp);

			// Token: 0x0600393C RID: 14652
			uint GetMethodSemantics(uint mb, uint tkEventProp, out uint pdwSemanticsFlags);

			// Token: 0x0600393D RID: 14653
			uint GetClassLayout(uint td, out uint pdwPackSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] long[] rFieldOffset, uint cMax, out uint pcFieldOffset, out uint pulClassSize);

			// Token: 0x0600393E RID: 14654
			uint GetFieldMarshal(uint tk, out IntPtr ppvNativeType, out uint pcbNativeType);

			// Token: 0x0600393F RID: 14655
			uint GetRVA(uint tk, out uint pulCodeRVA, out uint pdwImplFlags);

			// Token: 0x06003940 RID: 14656
			uint GetPermissionSetProps(uint pm, out uint pdwAction, out IntPtr ppvPermission, out uint pcbPermission);

			// Token: 0x06003941 RID: 14657
			uint GetSigFromToken(uint mdSig, out IntPtr ppvSig, out uint pcbSig);

			// Token: 0x06003942 RID: 14658
			uint GetModuleRefProps(uint mur, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] char[] szName, uint cchName, out uint pchName);

			// Token: 0x06003943 RID: 14659
			uint EnumModuleRefs(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] uint[] rModuleRefs, uint cmax, out uint pcModuleRefs);

			// Token: 0x06003944 RID: 14660
			uint GetTypeSpecFromToken(uint typespec, out IntPtr ppvSig, out uint pcbSig);

			// Token: 0x06003945 RID: 14661
			uint GetNameFromToken(uint tk, out IntPtr pszUtf8NamePtr);

			// Token: 0x06003946 RID: 14662
			uint EnumUnresolvedMethods(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] uint[] rMethods, uint cMax, out uint pcTokens);

			// Token: 0x06003947 RID: 14663
			uint GetUserString(uint stk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] char[] szString, uint cchString, out uint pchString);

			// Token: 0x06003948 RID: 14664
			uint GetPinvokeMap(uint tk, out uint pdwMappingFlags, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] szImportName, uint cchImportName, out uint pchImportName, out uint pmrImportDLL);

			// Token: 0x06003949 RID: 14665
			uint EnumSignatures(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] uint[] rSignatures, uint cmax, out uint pcSignatures);

			// Token: 0x0600394A RID: 14666
			uint EnumTypeSpecs(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] uint[] rTypeSpecs, uint cmax, out uint pcTypeSpecs);

			// Token: 0x0600394B RID: 14667
			uint EnumUserStrings(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] uint[] rStrings, uint cmax, out uint pcStrings);

			// Token: 0x0600394C RID: 14668
			uint GetParamForMethodIndex(uint md, uint ulParamSeq, out uint ppd);

			// Token: 0x0600394D RID: 14669
			uint EnumCustomAttributes(ref uint phEnum, uint tk, uint tkType, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] uint[] rCustomAttributes, uint cMax, out uint pcCustomAttributes);

			// Token: 0x0600394E RID: 14670
			uint GetCustomAttributeProps(uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob, out uint pcbSize);

			// Token: 0x0600394F RID: 14671
			uint FindTypeRef(uint tkResolutionScope, [MarshalAs(UnmanagedType.LPWStr)] string szName, out uint ptr);

			// Token: 0x06003950 RID: 14672
			uint GetMemberProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

			// Token: 0x06003951 RID: 14673
			uint GetFieldProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] szField, uint cchField, out uint pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

			// Token: 0x06003952 RID: 14674
			uint GetPropertyProps(uint prop, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] szProperty, uint cchProperty, out uint pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 13)] uint[] rmdOtherMethod, uint cMax, out uint pcOtherMethod);

			// Token: 0x06003953 RID: 14675
			uint GetParamProps(uint tk, out uint pmd, out uint pulSequence, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] char[] szName, uint cchName, out uint pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

			// Token: 0x06003954 RID: 14676
			uint GetCustomAttributeByName(uint tkObj, [MarshalAs(UnmanagedType.LPWStr)] string szName, out IntPtr ppData, out uint pcbData);

			// Token: 0x06003955 RID: 14677
			bool IsValidToken(uint tk);

			// Token: 0x06003956 RID: 14678
			uint GetNestedClassProps(uint tdNestedClass, out uint ptdEnclosingClass);

			// Token: 0x06003957 RID: 14679
			uint GetNativeCallConvFromSig(IntPtr pvSig, uint cbSig, out uint pCallConv);

			// Token: 0x06003958 RID: 14680
			uint IsGlobal(uint pd, out uint pbGlobal);
		}
	}
}
