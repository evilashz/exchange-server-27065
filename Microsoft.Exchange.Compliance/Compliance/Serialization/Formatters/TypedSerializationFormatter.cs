using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Compliance.Serialization.Formatters
{
	// Token: 0x0200000A RID: 10
	internal class TypedSerializationFormatter
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000040CC File Offset: 0x000022CC
		public static void WriteTypeEvent(Type type, bool allowed)
		{
			if (allowed)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder("The following types were discovered in your serialized stream.\r\n\r\n");
			if (type == null)
			{
				stringBuilder.AppendLine("There were no Types discovered for this serialization object");
			}
			else
			{
				stringBuilder.AppendLine(type.FullName);
			}
			stringBuilder.AppendLine("\r\n\r\nStackTrace for the current call");
			stringBuilder.AppendLine(new StackTrace(true).ToString());
			using (EventLog eventLog = new EventLog("Application"))
			{
				eventLog.Source = "MSExchange Common";
				eventLog.WriteEntry(stringBuilder.ToString(), EventLogEntryType.Information, 8675);
			}
		}

		// Token: 0x0400000D RID: 13
		protected const string Prologue = "The following types were discovered in your serialized stream.\r\n\r\n";

		// Token: 0x0400000E RID: 14
		protected const string NoTypes = "There were no Types discovered for this serialization object";

		// Token: 0x0400000F RID: 15
		protected const string StackIndicator = "\r\n\r\nStackTrace for the current call";

		// Token: 0x04000010 RID: 16
		protected const string EventLog = "Application";

		// Token: 0x04000011 RID: 17
		protected const string EventSource = "MSExchange Common";

		// Token: 0x04000012 RID: 18
		private const int EventId = 8675;

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000046 RID: 70
		public delegate void TypeEncounteredDelegate(Type type, bool allowed);

		// Token: 0x0200000C RID: 12
		internal sealed class TypeBinder : SerializationBinder
		{
			// Token: 0x06000049 RID: 73 RVA: 0x00004178 File Offset: 0x00002378
			public TypeBinder(TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered, bool strict)
			{
				this.typeEncounteredCallback = typeEncountered;
				this.strict = strict;
				this.expected = TypedSerializationFormatter.TypeBinder.GenerateExpected(null);
			}

			// Token: 0x0600004A RID: 74 RVA: 0x000041A1 File Offset: 0x000023A1
			public TypeBinder(Dictionary<Type, string> expectedTypes, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered)
			{
				this.expected = expectedTypes;
				this.typeEncounteredCallback = typeEncountered;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x000041BE File Offset: 0x000023BE
			public TypeBinder(Type[] expectedTypes, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered, bool strict)
			{
				this.expected = TypedSerializationFormatter.TypeBinder.GenerateExpected(expectedTypes);
				this.typeEncounteredCallback = typeEncountered;
				this.strict = strict;
			}

			// Token: 0x0600004C RID: 76 RVA: 0x000041E7 File Offset: 0x000023E7
			public TypeBinder(Type[] expectedTypes, Type[] baseClasses, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered, bool strict) : this(expectedTypes, baseClasses, null, typeEncountered, strict)
			{
			}

			// Token: 0x0600004D RID: 77 RVA: 0x000041F8 File Offset: 0x000023F8
			public TypeBinder(Type[] expectedTypes, Type[] baseClasses, Type[] genericTypes, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered, bool strict)
			{
				if (baseClasses != null && baseClasses.Length > 0)
				{
					this.baseClassSet = baseClasses;
				}
				if (genericTypes != null && genericTypes.Length > 0)
				{
					this.genericTypeSet = new HashSet<Type>();
					foreach (Type type in genericTypes)
					{
						if (type.IsGenericTypeDefinition && !this.genericTypeSet.Contains(type))
						{
							this.genericTypeSet.Add(type);
						}
					}
				}
				this.expected = TypedSerializationFormatter.TypeBinder.GenerateExpected(expectedTypes);
				this.typeEncounteredCallback = typeEncountered;
				this.strict = strict;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600004E RID: 78 RVA: 0x00004289 File Offset: 0x00002489
			public bool IsInitialized
			{
				get
				{
					return this.expected != null && this.expected.Count != 0;
				}
			}

			// Token: 0x0600004F RID: 79 RVA: 0x000042A4 File Offset: 0x000024A4
			public static Dictionary<Type, string> GenerateExpected(Type[] data)
			{
				Dictionary<Type, string> dictionary = new Dictionary<Type, string>(TypedSerializationFormatter.TypeBinder.Whitelist.Count);
				foreach (Type type in TypedSerializationFormatter.TypeBinder.Whitelist)
				{
					dictionary.Add(type, type.Name);
				}
				if (data != null && data.Length > 0)
				{
					foreach (Type type2 in data)
					{
						if (!dictionary.ContainsKey(type2))
						{
							dictionary.Add(type2, type2.Name);
						}
					}
				}
				return dictionary;
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00004348 File Offset: 0x00002548
			public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
			{
				base.BindToName(serializedType, out assemblyName, out typeName);
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00004354 File Offset: 0x00002554
			public override Type BindToType(string assemblyName, string typeName)
			{
				if (string.IsNullOrEmpty(typeName))
				{
					throw new BlockedTypeException("Null");
				}
				Type type = Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
				if (type == null)
				{
					string text = assemblyName.Split(new char[]
					{
						','
					})[0];
					type = Type.GetType(string.Format("{0}, {1}", typeName, text));
					if (type == null)
					{
						Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
						foreach (Assembly assembly in assemblies)
						{
							if (text == assembly.FullName.Split(new char[]
							{
								','
							})[0])
							{
								type = assembly.GetType(typeName);
								break;
							}
						}
					}
				}
				if (type == null)
				{
					throw new BlockedTypeException(typeName + "," + assemblyName);
				}
				this.CheckValidity(type);
				return type;
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00004440 File Offset: 0x00002640
			private void CheckValidity(Type unknown)
			{
				if (unknown != null)
				{
					if (this.strict)
					{
						if (this.expected.ContainsKey(unknown))
						{
							return;
						}
					}
					else if (!this.strict && this.expected.ContainsValue(unknown.Name))
					{
						return;
					}
					if (unknown.IsConstructedGenericType && this.genericTypeSet != null && this.genericTypeSet.Contains(unknown.GetGenericTypeDefinition()))
					{
						return;
					}
					if (this.baseClassSet != null)
					{
						foreach (Type type in this.baseClassSet)
						{
							if (!(type.Name == "Object") && unknown.IsSubclassOf(type))
							{
								return;
							}
						}
					}
				}
				if (this.typeEncounteredCallback != null)
				{
					this.typeEncounteredCallback(unknown, false);
				}
				throw new BlockedTypeException((unknown != null) ? unknown.ToString() : "Null");
			}

			// Token: 0x04000013 RID: 19
			private const string Null = "Null";

			// Token: 0x04000014 RID: 20
			private const string ObjectName = "Object";

			// Token: 0x04000015 RID: 21
			private const string TypeFormat = "{0}, {1}";

			// Token: 0x04000016 RID: 22
			private static readonly List<Type> Whitelist = new List<Type>(new Type[]
			{
				typeof(string),
				typeof(int),
				typeof(uint),
				typeof(long),
				typeof(ulong),
				typeof(double),
				typeof(float),
				typeof(bool),
				typeof(short),
				typeof(ushort),
				typeof(byte),
				typeof(char)
			});

			// Token: 0x04000017 RID: 23
			private readonly bool strict = true;

			// Token: 0x04000018 RID: 24
			private Dictionary<Type, string> expected;

			// Token: 0x04000019 RID: 25
			private Type[] baseClassSet;

			// Token: 0x0400001A RID: 26
			private HashSet<Type> genericTypeSet;

			// Token: 0x0400001B RID: 27
			private TypedSerializationFormatter.TypeEncounteredDelegate typeEncounteredCallback;
		}
	}
}
