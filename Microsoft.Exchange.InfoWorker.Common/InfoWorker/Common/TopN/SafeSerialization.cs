using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.TopN
{
	// Token: 0x0200027E RID: 638
	public class SafeSerialization
	{
		// Token: 0x0600124B RID: 4683 RVA: 0x00055378 File Offset: 0x00053578
		public static object SafeBinaryFormatterDeserializeWithAllowList(Stream stream, IEnumerable<Type> allowList, SafeSerialization.TypeEncounteredDelegate typeEncounteredCallback = null)
		{
			SafeSerialization.ValidatingBinder binder = new SafeSerialization.ValidatingBinder(new SafeSerialization.AllowList(allowList), typeEncounteredCallback);
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			binaryFormatter.Binder = binder;
			return binaryFormatter.Deserialize(stream);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x000553A7 File Offset: 0x000535A7
		public static bool IsSafeBinaryFormatterStreamWithAllowList(Stream serializationStream, IEnumerable<Type> allowList, SafeSerialization.TypeEncounteredDelegate typeEncounteredCallback = null)
		{
			return SafeSerialization.IsSafeBinaryFormatterStreamCommon(new SafeSerialization.ValidatingBinder(new SafeSerialization.AllowList(allowList), typeEncounteredCallback), serializationStream);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000553BC File Offset: 0x000535BC
		private static bool IsSafeBinaryFormatterStreamCommon(SafeSerialization.ValidatingBinder binder, Stream serializationStream)
		{
			long position = serializationStream.Position;
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			try
			{
				binaryFormatter.Binder = binder;
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

		// Token: 0x0200027F RID: 639
		// (Invoke) Token: 0x06001250 RID: 4688
		public delegate void TypeEncounteredDelegate(string fullName, SafeSerialization.FilterDecision decision);

		// Token: 0x02000280 RID: 640
		public enum FilterDecision
		{
			// Token: 0x04000C01 RID: 3073
			Allow,
			// Token: 0x04000C02 RID: 3074
			Deny
		}

		// Token: 0x02000281 RID: 641
		public class AllowList
		{
			// Token: 0x06001253 RID: 4691 RVA: 0x00055420 File Offset: 0x00053620
			public AllowList(IEnumerable<Type> allowList)
			{
				this.List = allowList;
			}

			// Token: 0x1700049C RID: 1180
			// (get) Token: 0x06001254 RID: 4692 RVA: 0x0005542F File Offset: 0x0005362F
			// (set) Token: 0x06001255 RID: 4693 RVA: 0x00055437 File Offset: 0x00053637
			public IEnumerable<Type> List { get; private set; }
		}

		// Token: 0x02000282 RID: 642
		[Serializable]
		public sealed class BlockedTypeException : ApplicationException
		{
			// Token: 0x06001256 RID: 4694 RVA: 0x00055440 File Offset: 0x00053640
			public BlockedTypeException(string message) : base(message)
			{
			}

			// Token: 0x06001257 RID: 4695 RVA: 0x00055449 File Offset: 0x00053649
			private BlockedTypeException(SerializationInfo si, StreamingContext sc) : base(si, sc)
			{
			}
		}

		// Token: 0x02000283 RID: 643
		private sealed class ValidatingBinder : SerializationBinder
		{
			// Token: 0x06001258 RID: 4696 RVA: 0x00055453 File Offset: 0x00053653
			public ValidatingBinder(SafeSerialization.AllowList allowList, SafeSerialization.TypeEncounteredDelegate typeEncounteredCallback)
			{
				this.allowedTypes = ((allowList != null) ? new HashSet<Type>(allowList.List) : new HashSet<Type>());
				this.typeFoundCallback = typeEncounteredCallback;
			}

			// Token: 0x06001259 RID: 4697 RVA: 0x0005547D File Offset: 0x0005367D
			public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600125A RID: 4698 RVA: 0x00055484 File Offset: 0x00053684
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

			// Token: 0x04000C04 RID: 3076
			private HashSet<Type> allowedTypes;

			// Token: 0x04000C05 RID: 3077
			private SafeSerialization.TypeEncounteredDelegate typeFoundCallback;
		}
	}
}
