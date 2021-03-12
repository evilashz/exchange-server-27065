using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.Serialization.Formatters
{
	// Token: 0x0200000D RID: 13
	internal sealed class TypedBinaryFormatter : TypedSerializationFormatter
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000045DF File Offset: 0x000027DF
		public static object DeserializeObject(Stream serializationStream, TypedSerializationFormatter.TypeBinder binder)
		{
			if (!binder.IsInitialized)
			{
				throw new ArgumentException("binder", "Binder must be initialized before use");
			}
			return TypedBinaryFormatter.Deserialize(serializationStream, binder);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004600 File Offset: 0x00002800
		public static object DeserializeObject(Stream serializationStream, Type[] expectedTypes, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered, bool strict)
		{
			return TypedBinaryFormatter.Deserialize(serializationStream, new TypedSerializationFormatter.TypeBinder(expectedTypes, typeEncountered, strict));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004610 File Offset: 0x00002810
		public static object DeserializeObject(Stream serializationStream, Type[] expectedTypes, Type[] baseClasses, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered, bool strict)
		{
			return TypedBinaryFormatter.Deserialize(serializationStream, new TypedSerializationFormatter.TypeBinder(expectedTypes, baseClasses, typeEncountered, strict));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004622 File Offset: 0x00002822
		public static object DeserializeObject(Stream serializationStream, Dictionary<Type, string> expectedTypes, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered)
		{
			if (expectedTypes == null || expectedTypes.Count == 0)
			{
				throw new ArgumentException("expectedTypes", "ExpectedTypes must be initialized before use");
			}
			return TypedBinaryFormatter.Deserialize(serializationStream, new TypedSerializationFormatter.TypeBinder(expectedTypes, typeEncountered));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000464C File Offset: 0x0000284C
		private static object Deserialize(Stream serializationStream, SerializationBinder binder)
		{
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(binder, new string[]
			{
				"System.DelegateSerializationHolder"
			});
			return binaryFormatter.Deserialize(serializationStream);
		}
	}
}
