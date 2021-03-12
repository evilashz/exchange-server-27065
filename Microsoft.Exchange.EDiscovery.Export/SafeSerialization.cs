using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200002B RID: 43
	public class SafeSerialization
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public static object SafeBinaryFormatterDeserializeWithAllowList(Stream stream, IEnumerable<Type> allowList, SafeSerialization.TypeEncounteredDelegate typeEncounteredCallback = null)
		{
			SafeSerialization.ValidatingBinder binder = new SafeSerialization.ValidatingBinder(new SafeSerialization.AllowList(allowList), typeEncounteredCallback);
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(binder);
			return binaryFormatter.Deserialize(stream);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005C0C File Offset: 0x00003E0C
		public static bool IsSafeBinaryFormatterStreamWithAllowList(Stream serializationStream, IEnumerable<Type> allowList, SafeSerialization.TypeEncounteredDelegate typeEncounteredCallback = null)
		{
			return SafeSerialization.IsSafeBinaryFormatterStreamCommon(new SafeSerialization.ValidatingBinder(new SafeSerialization.AllowList(allowList), typeEncounteredCallback), serializationStream);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005C20 File Offset: 0x00003E20
		private static bool IsSafeBinaryFormatterStreamCommon(SafeSerialization.ValidatingBinder binder, Stream serializationStream)
		{
			long position = serializationStream.Position;
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(binder);
			try
			{
				binaryFormatter.Deserialize(serializationStream);
			}
			catch (SafeSerialization.BlockedTypeException)
			{
				return false;
			}
			finally
			{
				serializationStream.Seek(position, SeekOrigin.Begin);
			}
			return true;
		}

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x06000184 RID: 388
		public delegate void TypeEncounteredDelegate(string fullName, SafeSerialization.FilterDecision decision);

		// Token: 0x0200002D RID: 45
		public enum FilterDecision
		{
			// Token: 0x040000A0 RID: 160
			Allow,
			// Token: 0x040000A1 RID: 161
			Deny
		}

		// Token: 0x0200002E RID: 46
		public class AllowList
		{
			// Token: 0x06000187 RID: 391 RVA: 0x00005C80 File Offset: 0x00003E80
			public AllowList(IEnumerable<Type> allowList)
			{
				this.List = allowList;
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000188 RID: 392 RVA: 0x00005C8F File Offset: 0x00003E8F
			// (set) Token: 0x06000189 RID: 393 RVA: 0x00005C97 File Offset: 0x00003E97
			public IEnumerable<Type> List { get; private set; }
		}

		// Token: 0x0200002F RID: 47
		[Serializable]
		public sealed class BlockedTypeException : ApplicationException
		{
			// Token: 0x0600018A RID: 394 RVA: 0x00005CA0 File Offset: 0x00003EA0
			public BlockedTypeException(string message) : base(message)
			{
			}

			// Token: 0x0600018B RID: 395 RVA: 0x00005CA9 File Offset: 0x00003EA9
			private BlockedTypeException(SerializationInfo si, StreamingContext sc) : base(si, sc)
			{
			}
		}

		// Token: 0x02000030 RID: 48
		private sealed class ValidatingBinder : SerializationBinder
		{
			// Token: 0x0600018C RID: 396 RVA: 0x00005CB3 File Offset: 0x00003EB3
			public ValidatingBinder(SafeSerialization.AllowList allowList, SafeSerialization.TypeEncounteredDelegate typeEncounteredCallback)
			{
				this.allowedTypes = ((allowList != null) ? new HashSet<Type>(allowList.List) : new HashSet<Type>());
				this.typeFoundCallback = typeEncounteredCallback;
			}

			// Token: 0x0600018D RID: 397 RVA: 0x00005CDD File Offset: 0x00003EDD
			public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600018E RID: 398 RVA: 0x00005CE4 File Offset: 0x00003EE4
			public override Type BindToType(string assemblyName, string typeName)
			{
				Type type = Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", new object[]
				{
					typeName,
					assemblyName
				}));
				string text = (type != null) ? type.FullName : string.Empty;
				SafeSerialization.FilterDecision filterDecision = SafeSerialization.FilterDecision.Deny;
				if (type != null && this.allowedTypes.Contains(type))
				{
					filterDecision = SafeSerialization.FilterDecision.Allow;
				}
				if (this.typeFoundCallback != null)
				{
					this.typeFoundCallback(text, filterDecision);
				}
				if (filterDecision == SafeSerialization.FilterDecision.Deny)
				{
					throw new SafeSerialization.BlockedTypeException(text);
				}
				return type;
			}

			// Token: 0x040000A3 RID: 163
			private HashSet<Type> allowedTypes;

			// Token: 0x040000A4 RID: 164
			private SafeSerialization.TypeEncounteredDelegate typeFoundCallback;
		}
	}
}
