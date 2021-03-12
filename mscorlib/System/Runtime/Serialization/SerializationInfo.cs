using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000714 RID: 1812
	[ComVisible(true)]
	public sealed class SerializationInfo
	{
		// Token: 0x060050CB RID: 20683 RVA: 0x0011B5AE File Offset: 0x001197AE
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter) : this(type, converter, false)
		{
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x0011B5BC File Offset: 0x001197BC
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter, bool requireSameTokenInPartialTrust)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			this.objectType = type;
			this.m_fullTypeName = type.FullName;
			this.m_assemName = type.Module.Assembly.FullName;
			this.m_members = new string[4];
			this.m_data = new object[4];
			this.m_types = new Type[4];
			this.m_nameToIndex = new Dictionary<string, int>();
			this.m_converter = converter;
			this.requireSameTokenInPartialTrust = requireSameTokenInPartialTrust;
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060050CD RID: 20685 RVA: 0x0011B651 File Offset: 0x00119851
		// (set) Token: 0x060050CE RID: 20686 RVA: 0x0011B659 File Offset: 0x00119859
		public string FullTypeName
		{
			get
			{
				return this.m_fullTypeName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_fullTypeName = value;
				this.isFullTypeNameSetExplicit = true;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060050CF RID: 20687 RVA: 0x0011B677 File Offset: 0x00119877
		// (set) Token: 0x060050D0 RID: 20688 RVA: 0x0011B67F File Offset: 0x0011987F
		public string AssemblyName
		{
			get
			{
				return this.m_assemName;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.requireSameTokenInPartialTrust)
				{
					SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.m_assemName, value);
				}
				this.m_assemName = value;
				this.isAssemblyNameSetExplicit = true;
			}
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x0011B6B4 File Offset: 0x001198B4
		[SecuritySafeCritical]
		public void SetType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.requireSameTokenInPartialTrust)
			{
				SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.ObjectType.Assembly.FullName, type.Assembly.FullName);
			}
			if (this.objectType != type)
			{
				this.objectType = type;
				this.m_fullTypeName = type.FullName;
				this.m_assemName = type.Module.Assembly.FullName;
				this.isFullTypeNameSetExplicit = false;
				this.isAssemblyNameSetExplicit = false;
			}
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x0011B738 File Offset: 0x00119938
		private static bool Compare(byte[] a, byte[] b)
		{
			if (a == null || b == null || a.Length == 0 || b.Length == 0 || a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x0011B776 File Offset: 0x00119976
		[SecuritySafeCritical]
		internal static void DemandForUnsafeAssemblyNameAssignments(string originalAssemblyName, string newAssemblyName)
		{
			if (!SerializationInfo.IsAssemblyNameAssignmentSafe(originalAssemblyName, newAssemblyName))
			{
				CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
			}
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0011B788 File Offset: 0x00119988
		internal static bool IsAssemblyNameAssignmentSafe(string originalAssemblyName, string newAssemblyName)
		{
			if (originalAssemblyName == newAssemblyName)
			{
				return true;
			}
			AssemblyName assemblyName = new AssemblyName(originalAssemblyName);
			AssemblyName assemblyName2 = new AssemblyName(newAssemblyName);
			return !string.Equals(assemblyName2.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) && !string.Equals(assemblyName2.Name, "mscorlib.dll", StringComparison.OrdinalIgnoreCase) && SerializationInfo.Compare(assemblyName.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken());
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060050D5 RID: 20693 RVA: 0x0011B7E7 File Offset: 0x001199E7
		public int MemberCount
		{
			get
			{
				return this.m_currMember;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060050D6 RID: 20694 RVA: 0x0011B7EF File Offset: 0x001199EF
		public Type ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060050D7 RID: 20695 RVA: 0x0011B7F7 File Offset: 0x001199F7
		public bool IsFullTypeNameSetExplicit
		{
			get
			{
				return this.isFullTypeNameSetExplicit;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060050D8 RID: 20696 RVA: 0x0011B7FF File Offset: 0x001199FF
		public bool IsAssemblyNameSetExplicit
		{
			get
			{
				return this.isAssemblyNameSetExplicit;
			}
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x0011B807 File Offset: 0x00119A07
		public SerializationInfoEnumerator GetEnumerator()
		{
			return new SerializationInfoEnumerator(this.m_members, this.m_data, this.m_types, this.m_currMember);
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x0011B828 File Offset: 0x00119A28
		private void ExpandArrays()
		{
			int num = this.m_currMember * 2;
			if (num < this.m_currMember && 2147483647 > this.m_currMember)
			{
				num = int.MaxValue;
			}
			string[] array = new string[num];
			object[] array2 = new object[num];
			Type[] array3 = new Type[num];
			Array.Copy(this.m_members, array, this.m_currMember);
			Array.Copy(this.m_data, array2, this.m_currMember);
			Array.Copy(this.m_types, array3, this.m_currMember);
			this.m_members = array;
			this.m_data = array2;
			this.m_types = array3;
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x0011B8BA File Offset: 0x00119ABA
		public void AddValue(string name, object value, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.AddValueInternal(name, value, type);
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x0011B8E1 File Offset: 0x00119AE1
		public void AddValue(string name, object value)
		{
			if (value == null)
			{
				this.AddValue(name, value, typeof(object));
				return;
			}
			this.AddValue(name, value, value.GetType());
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0011B907 File Offset: 0x00119B07
		public void AddValue(string name, bool value)
		{
			this.AddValue(name, value, typeof(bool));
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x0011B920 File Offset: 0x00119B20
		public void AddValue(string name, char value)
		{
			this.AddValue(name, value, typeof(char));
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x0011B939 File Offset: 0x00119B39
		[CLSCompliant(false)]
		public void AddValue(string name, sbyte value)
		{
			this.AddValue(name, value, typeof(sbyte));
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x0011B952 File Offset: 0x00119B52
		public void AddValue(string name, byte value)
		{
			this.AddValue(name, value, typeof(byte));
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x0011B96B File Offset: 0x00119B6B
		public void AddValue(string name, short value)
		{
			this.AddValue(name, value, typeof(short));
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0011B984 File Offset: 0x00119B84
		[CLSCompliant(false)]
		public void AddValue(string name, ushort value)
		{
			this.AddValue(name, value, typeof(ushort));
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x0011B99D File Offset: 0x00119B9D
		public void AddValue(string name, int value)
		{
			this.AddValue(name, value, typeof(int));
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x0011B9B6 File Offset: 0x00119BB6
		[CLSCompliant(false)]
		public void AddValue(string name, uint value)
		{
			this.AddValue(name, value, typeof(uint));
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x0011B9CF File Offset: 0x00119BCF
		public void AddValue(string name, long value)
		{
			this.AddValue(name, value, typeof(long));
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x0011B9E8 File Offset: 0x00119BE8
		[CLSCompliant(false)]
		public void AddValue(string name, ulong value)
		{
			this.AddValue(name, value, typeof(ulong));
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x0011BA01 File Offset: 0x00119C01
		public void AddValue(string name, float value)
		{
			this.AddValue(name, value, typeof(float));
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x0011BA1A File Offset: 0x00119C1A
		public void AddValue(string name, double value)
		{
			this.AddValue(name, value, typeof(double));
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x0011BA33 File Offset: 0x00119C33
		public void AddValue(string name, decimal value)
		{
			this.AddValue(name, value, typeof(decimal));
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x0011BA4C File Offset: 0x00119C4C
		public void AddValue(string name, DateTime value)
		{
			this.AddValue(name, value, typeof(DateTime));
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x0011BA68 File Offset: 0x00119C68
		internal void AddValueInternal(string name, object value, Type type)
		{
			if (this.m_nameToIndex.ContainsKey(name))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_SameNameTwice"));
			}
			this.m_nameToIndex.Add(name, this.m_currMember);
			if (this.m_currMember >= this.m_members.Length)
			{
				this.ExpandArrays();
			}
			this.m_members[this.m_currMember] = name;
			this.m_data[this.m_currMember] = value;
			this.m_types[this.m_currMember] = type;
			this.m_currMember++;
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0011BAF4 File Offset: 0x00119CF4
		internal void UpdateValue(string name, object value, Type type)
		{
			int num = this.FindElement(name);
			if (num < 0)
			{
				this.AddValueInternal(name, value, type);
				return;
			}
			this.m_data[num] = value;
			this.m_types[num] = type;
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x0011BB2C File Offset: 0x00119D2C
		private int FindElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int result;
			if (this.m_nameToIndex.TryGetValue(name, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x0011BB5C File Offset: 0x00119D5C
		private object GetElement(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NotFound", new object[]
				{
					name
				}));
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x0011BBA4 File Offset: 0x00119DA4
		[ComVisible(true)]
		private object GetElementNoThrow(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				foundType = null;
				return null;
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x0011BBD4 File Offset: 0x00119DD4
		[SecuritySafeCritical]
		public object GetValue(string name, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			Type type2;
			object element = this.GetElement(name, out type2);
			if (RemotingServices.IsTransparentProxy(element))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(element);
				if (RemotingServices.ProxyCheckCast(realProxy, runtimeType))
				{
					return element;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || element == null)
			{
				return element;
			}
			return this.m_converter.Convert(element, type);
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x0011BC54 File Offset: 0x00119E54
		[SecuritySafeCritical]
		[ComVisible(true)]
		internal object GetValueNoThrow(string name, Type type)
		{
			Type type2;
			object elementNoThrow = this.GetElementNoThrow(name, out type2);
			if (elementNoThrow == null)
			{
				return null;
			}
			if (RemotingServices.IsTransparentProxy(elementNoThrow))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(elementNoThrow);
				if (RemotingServices.ProxyCheckCast(realProxy, (RuntimeType)type))
				{
					return elementNoThrow;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || elementNoThrow == null)
			{
				return elementNoThrow;
			}
			return this.m_converter.Convert(elementNoThrow, type);
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x0011BCB0 File Offset: 0x00119EB0
		public bool GetBoolean(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(bool))
			{
				return (bool)element;
			}
			return this.m_converter.ToBoolean(element);
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x0011BCE8 File Offset: 0x00119EE8
		public char GetChar(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(char))
			{
				return (char)element;
			}
			return this.m_converter.ToChar(element);
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x0011BD20 File Offset: 0x00119F20
		[CLSCompliant(false)]
		public sbyte GetSByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(sbyte))
			{
				return (sbyte)element;
			}
			return this.m_converter.ToSByte(element);
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x0011BD58 File Offset: 0x00119F58
		public byte GetByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(byte))
			{
				return (byte)element;
			}
			return this.m_converter.ToByte(element);
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x0011BD90 File Offset: 0x00119F90
		public short GetInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(short))
			{
				return (short)element;
			}
			return this.m_converter.ToInt16(element);
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x0011BDC8 File Offset: 0x00119FC8
		[CLSCompliant(false)]
		public ushort GetUInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(ushort))
			{
				return (ushort)element;
			}
			return this.m_converter.ToUInt16(element);
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x0011BE00 File Offset: 0x0011A000
		public int GetInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(int))
			{
				return (int)element;
			}
			return this.m_converter.ToInt32(element);
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x0011BE38 File Offset: 0x0011A038
		[CLSCompliant(false)]
		public uint GetUInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(uint))
			{
				return (uint)element;
			}
			return this.m_converter.ToUInt32(element);
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x0011BE70 File Offset: 0x0011A070
		public long GetInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(long))
			{
				return (long)element;
			}
			return this.m_converter.ToInt64(element);
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x0011BEA8 File Offset: 0x0011A0A8
		[CLSCompliant(false)]
		public ulong GetUInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(ulong))
			{
				return (ulong)element;
			}
			return this.m_converter.ToUInt64(element);
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x0011BEE0 File Offset: 0x0011A0E0
		public float GetSingle(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(float))
			{
				return (float)element;
			}
			return this.m_converter.ToSingle(element);
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x0011BF18 File Offset: 0x0011A118
		public double GetDouble(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(double))
			{
				return (double)element;
			}
			return this.m_converter.ToDouble(element);
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x0011BF50 File Offset: 0x0011A150
		public decimal GetDecimal(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(decimal))
			{
				return (decimal)element;
			}
			return this.m_converter.ToDecimal(element);
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x0011BF88 File Offset: 0x0011A188
		public DateTime GetDateTime(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(DateTime))
			{
				return (DateTime)element;
			}
			return this.m_converter.ToDateTime(element);
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x0011BFC0 File Offset: 0x0011A1C0
		public string GetString(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(string) || element == null)
			{
				return (string)element;
			}
			return this.m_converter.ToString(element);
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06005101 RID: 20737 RVA: 0x0011BFFA File Offset: 0x0011A1FA
		internal string[] MemberNames
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x0011C002 File Offset: 0x0011A202
		internal object[] MemberValues
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x0400238E RID: 9102
		private const int defaultSize = 4;

		// Token: 0x0400238F RID: 9103
		private const string s_mscorlibAssemblySimpleName = "mscorlib";

		// Token: 0x04002390 RID: 9104
		private const string s_mscorlibFileName = "mscorlib.dll";

		// Token: 0x04002391 RID: 9105
		internal string[] m_members;

		// Token: 0x04002392 RID: 9106
		internal object[] m_data;

		// Token: 0x04002393 RID: 9107
		internal Type[] m_types;

		// Token: 0x04002394 RID: 9108
		private Dictionary<string, int> m_nameToIndex;

		// Token: 0x04002395 RID: 9109
		internal int m_currMember;

		// Token: 0x04002396 RID: 9110
		internal IFormatterConverter m_converter;

		// Token: 0x04002397 RID: 9111
		private string m_fullTypeName;

		// Token: 0x04002398 RID: 9112
		private string m_assemName;

		// Token: 0x04002399 RID: 9113
		private Type objectType;

		// Token: 0x0400239A RID: 9114
		private bool isFullTypeNameSetExplicit;

		// Token: 0x0400239B RID: 9115
		private bool isAssemblyNameSetExplicit;

		// Token: 0x0400239C RID: 9116
		private bool requireSameTokenInPartialTrust;
	}
}
