using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000F2 RID: 242
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceDataContractSurrogate : IDataContractSurrogate
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00014BC2 File Offset: 0x00012DC2
		public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
		{
			return null;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00014BC5 File Offset: 0x00012DC5
		public object GetCustomDataToExport(Type clrType, Type dataContractType)
		{
			return null;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public Type GetDataContractType(Type type)
		{
			if (typeof(ByteQuantifiedSize).IsAssignableFrom(type))
			{
				return typeof(ByteQuantifiedSizeSurrogate);
			}
			return type;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00014BE8 File Offset: 0x00012DE8
		public object GetDeserializedObject(object obj, Type targetType)
		{
			ByteQuantifiedSizeSurrogate byteQuantifiedSizeSurrogate = obj as ByteQuantifiedSizeSurrogate;
			if (byteQuantifiedSizeSurrogate != null)
			{
				return byteQuantifiedSizeSurrogate.ToByteQuantifiedSize();
			}
			return obj;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00014C0C File Offset: 0x00012E0C
		public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
		{
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00014C10 File Offset: 0x00012E10
		public object GetObjectToSerialize(object obj, Type targetType)
		{
			if (obj is ByteQuantifiedSize)
			{
				return new ByteQuantifiedSizeSurrogate((ByteQuantifiedSize)obj);
			}
			return obj;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00014C34 File Offset: 0x00012E34
		public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
		{
			if (typeName.Equals(typeof(ByteQuantifiedSizeSurrogate).Name))
			{
				return typeof(ByteQuantifiedSize);
			}
			return null;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00014C59 File Offset: 0x00012E59
		public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
		{
			return typeDeclaration;
		}
	}
}
