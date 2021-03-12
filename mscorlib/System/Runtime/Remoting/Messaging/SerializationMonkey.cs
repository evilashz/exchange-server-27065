using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000840 RID: 2112
	[Serializable]
	internal class SerializationMonkey : ISerializable, IFieldInfo
	{
		// Token: 0x06005AAA RID: 23210 RVA: 0x0013D675 File Offset: 0x0013B875
		[SecurityCritical]
		internal SerializationMonkey(SerializationInfo info, StreamingContext ctx)
		{
			this._obj.RootSetObjectData(info, ctx);
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x0013D68A File Offset: 0x0013B88A
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06005AAC RID: 23212 RVA: 0x0013D69B File Offset: 0x0013B89B
		// (set) Token: 0x06005AAD RID: 23213 RVA: 0x0013D6A3 File Offset: 0x0013B8A3
		public string[] FieldNames
		{
			[SecurityCritical]
			get
			{
				return this.fieldNames;
			}
			[SecurityCritical]
			set
			{
				this.fieldNames = value;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x0013D6AC File Offset: 0x0013B8AC
		// (set) Token: 0x06005AAF RID: 23215 RVA: 0x0013D6B4 File Offset: 0x0013B8B4
		public Type[] FieldTypes
		{
			[SecurityCritical]
			get
			{
				return this.fieldTypes;
			}
			[SecurityCritical]
			set
			{
				this.fieldTypes = value;
			}
		}

		// Token: 0x040028C4 RID: 10436
		internal ISerializationRootObject _obj;

		// Token: 0x040028C5 RID: 10437
		internal string[] fieldNames;

		// Token: 0x040028C6 RID: 10438
		internal Type[] fieldTypes;
	}
}
