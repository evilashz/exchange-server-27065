using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Clutter;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x02000344 RID: 836
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class CustomPropertySerializerFactory
	{
		// Token: 0x06002512 RID: 9490 RVA: 0x0009578C File Offset: 0x0009398C
		static CustomPropertySerializerFactory()
		{
			CustomPropertySerializerFactory.serializerVersions.Add(1, () => new CustomPropertySerializerV1());
			CustomPropertySerializerFactory.maxVersion = CustomPropertySerializerFactory.serializerVersions.Keys.Max();
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000957DF File Offset: 0x000939DF
		public static AbstractCustomPropertySerializer GetSerializer()
		{
			return CustomPropertySerializerFactory.GetSerializer(CustomPropertySerializerFactory.maxVersion);
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000957EC File Offset: 0x000939EC
		public static AbstractCustomPropertySerializer GetDeserializer(byte[] bytes)
		{
			ArgumentValidator.ThrowIfNull("bytes", bytes);
			if (bytes.Length < 3)
			{
				InferenceDiagnosticsLog.Log("CustomPropertySerializerFactory.GetDeserializer", string.Format("Cannot deserialize a byte array that does not have a header. Length of bytes: '{0}'", bytes.Length));
				return null;
			}
			int num = (int)bytes[0];
			int num2 = (int)bytes[1];
			if (num <= CustomPropertySerializerFactory.maxVersion)
			{
				return CustomPropertySerializerFactory.GetSerializer(num);
			}
			if (num2 <= CustomPropertySerializerFactory.maxVersion)
			{
				return CustomPropertySerializerFactory.GetSerializer(CustomPropertySerializerFactory.maxVersion);
			}
			InferenceDiagnosticsLog.Log("Activity.DeserializeCustomPropertiesDictionary", string.Format("Unable to find a serializer for deserializing. Version used for serializing '{0}', MinVersion supported '{1}', MaxVersion understood by factory '{2}'", num, num2, CustomPropertySerializerFactory.maxVersion));
			return null;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00095880 File Offset: 0x00093A80
		private static AbstractCustomPropertySerializer GetSerializer(int version)
		{
			Func<AbstractCustomPropertySerializer> func;
			bool flag = CustomPropertySerializerFactory.serializerVersions.TryGetValue(version, out func);
			if (flag)
			{
				return func();
			}
			return null;
		}

		// Token: 0x0400168C RID: 5772
		private static readonly Dictionary<int, Func<AbstractCustomPropertySerializer>> serializerVersions = new Dictionary<int, Func<AbstractCustomPropertySerializer>>();

		// Token: 0x0400168D RID: 5773
		private static readonly int maxVersion;
	}
}
