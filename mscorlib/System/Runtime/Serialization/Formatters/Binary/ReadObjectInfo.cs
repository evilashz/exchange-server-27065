using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000775 RID: 1909
	internal sealed class ReadObjectInfo
	{
		// Token: 0x060053B3 RID: 21427 RVA: 0x00128CFD File Offset: 0x00126EFD
		internal ReadObjectInfo()
		{
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x00128D05 File Offset: 0x00126F05
		internal void ObjectEnd()
		{
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x00128D07 File Offset: 0x00126F07
		internal void PrepareForReuse()
		{
			this.lastPosition = 0;
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x00128D10 File Offset: 0x00126F10
		[SecurityCritical]
		internal static ReadObjectInfo Create(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x00128D36 File Offset: 0x00126F36
		[SecurityCritical]
		internal void Init(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			this.InitReadConstructor(objectType, surrogateSelector, context);
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x00128D70 File Offset: 0x00126F70
		[SecurityCritical]
		internal static ReadObjectInfo Create(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, memberNames, memberTypes, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x00128D9C File Offset: 0x00126F9C
		[SecurityCritical]
		internal void Init(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.wireMemberNames = memberNames;
			this.wireMemberTypes = memberTypes;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			if (memberNames != null)
			{
				this.isNamed = true;
			}
			if (memberTypes != null)
			{
				this.isTyped = true;
			}
			if (objectType != null)
			{
				this.InitReadConstructor(objectType, surrogateSelector, context);
			}
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x00128E08 File Offset: 0x00127008
		[SecurityCritical]
		private void InitReadConstructor(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context)
		{
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.isSi = true;
			}
			if (this.isSi)
			{
				this.InitSiRead();
				return;
			}
			this.InitMemberInfo();
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x00128E7B File Offset: 0x0012707B
		private void InitSiRead()
		{
			if (this.memberTypesList != null)
			{
				this.memberTypesList = new List<Type>(20);
			}
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x00128E92 File Offset: 0x00127092
		private void InitNoMembers()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x00128EA8 File Offset: 0x001270A8
		[SecurityCritical]
		private void InitMemberInfo()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
			this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
			this.count = this.cache.memberInfos.Length;
			this.cache.memberNames = new string[this.count];
			this.cache.memberTypes = new Type[this.count];
			for (int i = 0; i < this.count; i++)
			{
				this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
				this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x00128F80 File Offset: 0x00127180
		internal MemberInfo GetMemberInfo(string name)
		{
			if (this.cache == null)
			{
				return null;
			}
			if (this.isSi)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_MemberInfo", new object[]
				{
					this.objectType + " " + name
				}));
			}
			if (this.cache.memberInfos == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NoMemberInfo", new object[]
				{
					this.objectType + " " + name
				}));
			}
			int num = this.Position(name);
			if (num != -1)
			{
				return this.cache.memberInfos[this.Position(name)];
			}
			return null;
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x00129024 File Offset: 0x00127224
		internal Type GetType(string name)
		{
			int num = this.Position(name);
			if (num == -1)
			{
				return null;
			}
			Type type;
			if (this.isTyped)
			{
				type = this.cache.memberTypes[num];
			}
			else
			{
				type = this.memberTypesList[num];
			}
			if (type == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableTypes", new object[]
				{
					this.objectType + " " + name
				}));
			}
			return type;
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x00129094 File Offset: 0x00127294
		internal void AddValue(string name, object value, ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				si.AddValue(name, value);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				memberData[num] = value;
			}
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x001290C8 File Offset: 0x001272C8
		internal void InitDataStore(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				if (si == null)
				{
					si = new SerializationInfo(this.objectType, this.formatterConverter);
					return;
				}
			}
			else if (memberData == null && this.cache != null)
			{
				memberData = new object[this.cache.memberNames.Length];
			}
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x00129118 File Offset: 0x00127318
		internal void RecordFixup(long objectId, string name, long idRef)
		{
			if (this.isSi)
			{
				this.objectManager.RecordDelayedFixup(objectId, name, idRef);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				this.objectManager.RecordFixup(objectId, this.cache.memberInfos[num], idRef);
			}
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00129162 File Offset: 0x00127362
		[SecurityCritical]
		internal void PopulateObjectMembers(object obj, object[] memberData)
		{
			if (!this.isSi && memberData != null)
			{
				FormatterServices.PopulateObjectMembers(obj, this.cache.memberInfos, memberData);
			}
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x00129184 File Offset: 0x00127384
		[Conditional("SER_LOGGING")]
		private void DumpPopulate(MemberInfo[] memberInfos, object[] memberData)
		{
			for (int i = 0; i < memberInfos.Length; i++)
			{
			}
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0012919F File Offset: 0x0012739F
		[Conditional("SER_LOGGING")]
		private void DumpPopulateSi()
		{
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x001291A4 File Offset: 0x001273A4
		private int Position(string name)
		{
			if (this.cache == null)
			{
				return -1;
			}
			if (this.cache.memberNames.Length != 0 && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			int num = this.lastPosition + 1;
			this.lastPosition = num;
			if (num < this.cache.memberNames.Length && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			for (int i = 0; i < this.cache.memberNames.Length; i++)
			{
				if (this.cache.memberNames[i].Equals(name))
				{
					this.lastPosition = i;
					return this.lastPosition;
				}
			}
			this.lastPosition = 0;
			return -1;
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00129270 File Offset: 0x00127470
		internal Type[] GetMemberTypes(string[] inMemberNames, Type objectType)
		{
			if (this.isSi)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableTypes", new object[]
				{
					objectType
				}));
			}
			if (this.cache == null)
			{
				return null;
			}
			if (this.cache.memberTypes == null)
			{
				this.cache.memberTypes = new Type[this.count];
				for (int i = 0; i < this.count; i++)
				{
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
			}
			bool flag = false;
			if (inMemberNames.Length < this.cache.memberInfos.Length)
			{
				flag = true;
			}
			Type[] array = new Type[this.cache.memberInfos.Length];
			for (int j = 0; j < this.cache.memberInfos.Length; j++)
			{
				if (!flag && inMemberNames[j].Equals(this.cache.memberInfos[j].Name))
				{
					array[j] = this.cache.memberTypes[j];
				}
				else
				{
					bool flag2 = false;
					for (int k = 0; k < inMemberNames.Length; k++)
					{
						if (this.cache.memberInfos[j].Name.Equals(inMemberNames[k]))
						{
							array[j] = this.cache.memberTypes[j];
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						object[] customAttributes = this.cache.memberInfos[j].GetCustomAttributes(typeof(OptionalFieldAttribute), false);
						if ((customAttributes == null || customAttributes.Length == 0) && !this.bSimpleAssembly)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_MissingMember", new object[]
							{
								this.cache.memberNames[j],
								objectType,
								typeof(OptionalFieldAttribute).FullName
							}));
						}
					}
				}
			}
			return array;
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x0012943C File Offset: 0x0012763C
		internal Type GetMemberType(MemberInfo objMember)
		{
			Type result;
			if (objMember is FieldInfo)
			{
				result = ((FieldInfo)objMember).FieldType;
			}
			else
			{
				if (!(objMember is PropertyInfo))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_SerMemberInfo", new object[]
					{
						objMember.GetType()
					}));
				}
				result = ((PropertyInfo)objMember).PropertyType;
			}
			return result;
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x00129498 File Offset: 0x00127698
		private static ReadObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			return new ReadObjectInfo
			{
				objectInfoId = Interlocked.Increment(ref ReadObjectInfo.readObjectInfoCounter)
			};
		}

		// Token: 0x04002637 RID: 9783
		internal int objectInfoId;

		// Token: 0x04002638 RID: 9784
		internal static int readObjectInfoCounter;

		// Token: 0x04002639 RID: 9785
		internal Type objectType;

		// Token: 0x0400263A RID: 9786
		internal ObjectManager objectManager;

		// Token: 0x0400263B RID: 9787
		internal int count;

		// Token: 0x0400263C RID: 9788
		internal bool isSi;

		// Token: 0x0400263D RID: 9789
		internal bool isNamed;

		// Token: 0x0400263E RID: 9790
		internal bool isTyped;

		// Token: 0x0400263F RID: 9791
		internal bool bSimpleAssembly;

		// Token: 0x04002640 RID: 9792
		internal SerObjectInfoCache cache;

		// Token: 0x04002641 RID: 9793
		internal string[] wireMemberNames;

		// Token: 0x04002642 RID: 9794
		internal Type[] wireMemberTypes;

		// Token: 0x04002643 RID: 9795
		private int lastPosition;

		// Token: 0x04002644 RID: 9796
		internal ISurrogateSelector surrogateSelector;

		// Token: 0x04002645 RID: 9797
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x04002646 RID: 9798
		internal StreamingContext context;

		// Token: 0x04002647 RID: 9799
		internal List<Type> memberTypesList;

		// Token: 0x04002648 RID: 9800
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x04002649 RID: 9801
		internal IFormatterConverter formatterConverter;
	}
}
