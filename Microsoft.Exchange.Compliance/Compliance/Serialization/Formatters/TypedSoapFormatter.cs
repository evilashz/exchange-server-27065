using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Microsoft.Exchange.Compliance.Serialization.Formatters
{
	// Token: 0x0200000E RID: 14
	internal sealed class TypedSoapFormatter : TypedSerializationFormatter
	{
		// Token: 0x0600005A RID: 90 RVA: 0x0000467F File Offset: 0x0000287F
		public static object DeserializeObject(Stream serializationStream, TypedSerializationFormatter.TypeBinder binder)
		{
			if (!binder.IsInitialized)
			{
				throw new ArgumentException("binder", "Binder must be initialized before use");
			}
			return TypedSoapFormatter.Deserialize(serializationStream, binder);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000046A0 File Offset: 0x000028A0
		public static object DeserializeObject(Stream serializationStream, Type[] expectedTypes, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered, bool strict)
		{
			return TypedSoapFormatter.Deserialize(serializationStream, new TypedSerializationFormatter.TypeBinder(expectedTypes, typeEncountered, strict));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000046B0 File Offset: 0x000028B0
		public static object DeserializeObject(Stream serializationStream, Type[] expectedTypes, Type[] baseClasses, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered, bool strict)
		{
			return TypedSoapFormatter.Deserialize(serializationStream, new TypedSerializationFormatter.TypeBinder(expectedTypes, baseClasses, typeEncountered, strict));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000046C2 File Offset: 0x000028C2
		public static object DeserializeObject(Stream serializationStream, Dictionary<Type, string> expectedTypes, TypedSerializationFormatter.TypeEncounteredDelegate typeEncountered)
		{
			if (expectedTypes == null || expectedTypes.Count == 0)
			{
				throw new ArgumentException("expectedTypes", "ExpectedTypes must be initialized before use");
			}
			return TypedSoapFormatter.Deserialize(serializationStream, new TypedSerializationFormatter.TypeBinder(expectedTypes, typeEncountered));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000046EC File Offset: 0x000028EC
		private static object Deserialize(Stream serializationStream, SerializationBinder binder)
		{
			return new SoapFormatter
			{
				Binder = binder
			}.Deserialize(serializationStream);
		}
	}
}
