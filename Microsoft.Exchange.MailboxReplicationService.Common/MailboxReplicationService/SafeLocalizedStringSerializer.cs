using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000198 RID: 408
	internal class SafeLocalizedStringSerializer
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x00022A18 File Offset: 0x00020C18
		public static byte[] SafeSerialize(LocalizedString localizedString)
		{
			if (localizedString.Equals(default(LocalizedString)))
			{
				return null;
			}
			byte[] result;
			try
			{
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(new SafeLocalizedStringSerializer.ValidatingBinder());
				using (MemoryStream memoryStream = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream, localizedString);
					result = memoryStream.ToArray();
				}
			}
			catch (BlockedTypeException ex)
			{
				SafeLocalizedStringSerializer.CreateWatson(ex);
				result = SafeLocalizedStringSerializer.SafeSerialize(new LocalizedString(localizedString.ToString()));
			}
			return result;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00022AAC File Offset: 0x00020CAC
		public static LocalizedString SafeDeserialize(byte[] bytes)
		{
			if (bytes == null)
			{
				return default(LocalizedString);
			}
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(new SafeLocalizedStringSerializer.ValidatingBinder());
			object obj;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				try
				{
					obj = binaryFormatter.Deserialize(memoryStream);
				}
				catch (BlockedTypeException ex)
				{
					SafeLocalizedStringSerializer.CreateWatson(ex);
					return ex.LocalizedString;
				}
			}
			if (!(obj is LocalizedString))
			{
				return new LocalizedString((obj == null) ? "null" : obj.ToString());
			}
			return (LocalizedString)obj;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00022B44 File Offset: 0x00020D44
		private static void CreateWatson(BlockedTypeException ex)
		{
			MrsTracer.Common.Error("Unhandled type in SafeLocalizedStringSerializer:\n{0}\n{1}", new object[]
			{
				CommonUtils.FullExceptionMessage(ex),
				ex.StackTrace
			});
			lock (SafeLocalizedStringSerializer.typesThatGeneratedWatson)
			{
				if (SafeLocalizedStringSerializer.typesThatGeneratedWatson.Contains(ex.Type))
				{
					return;
				}
				SafeLocalizedStringSerializer.typesThatGeneratedWatson.Add(ex.Type);
			}
			ExWatson.SendReport(ex);
		}

		// Token: 0x0400089B RID: 2203
		private static readonly HashSet<string> typesThatGeneratedWatson = new HashSet<string>();

		// Token: 0x02000199 RID: 409
		private sealed class ValidatingBinder : SerializationBinder
		{
			// Token: 0x06000F4E RID: 3918 RVA: 0x00022BF4 File Offset: 0x00020DF4
			public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
			{
				if (serializedType != null && !serializedType.IsEnum && !SafeLocalizedStringSerializer.ValidatingBinder.whiteList.Contains(serializedType))
				{
					MrsTracer.Common.Error("SafeLocalizedStringSerializer.BindToName: Type = '{0}'", new object[]
					{
						serializedType
					});
					throw new BlockedTypeException(serializedType.ToString());
				}
				base.BindToName(serializedType, out assemblyName, out typeName);
			}

			// Token: 0x06000F4F RID: 3919 RVA: 0x00022C50 File Offset: 0x00020E50
			public override Type BindToType(string assemblyName, string typeName)
			{
				Type type = null;
				try
				{
					type = Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
				}
				catch (Exception ex)
				{
					MrsTracer.Common.Error("SafeLocalizedStringSerializer.BindToType: failed to get the type: {0}", new object[]
					{
						ex
					});
				}
				if (type != null && !type.IsEnum && !SafeLocalizedStringSerializer.ValidatingBinder.whiteList.Contains(type))
				{
					MrsTracer.Common.Error("SafeLocalizedStringSerializer.BindToType: Failed type name = '{0}', assembly name = '{1}', type = '{2}'", new object[]
					{
						typeName,
						assemblyName,
						type
					});
					throw new BlockedTypeException(type.ToString());
				}
				return type;
			}

			// Token: 0x0400089C RID: 2204
			private static readonly HashSet<Type> whiteList = new HashSet<Type>(new Type[]
			{
				typeof(string),
				typeof(int),
				typeof(uint),
				typeof(long),
				typeof(ulong),
				typeof(double),
				typeof(LocalizedString),
				typeof(Guid),
				typeof(DateTime),
				typeof(TimeSpan),
				typeof(TimeZone),
				typeof(ExDateTime),
				typeof(ExTimeZone),
				typeof(float),
				typeof(bool),
				typeof(short),
				typeof(ushort),
				typeof(byte),
				typeof(char),
				typeof(object)
			});
		}
	}
}
