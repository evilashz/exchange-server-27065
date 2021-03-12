using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003F2 RID: 1010
	internal abstract class SmtpMessageContextBlob : IInboundMessageContextBlob
	{
		// Token: 0x06002E1B RID: 11803 RVA: 0x000B894E File Offset: 0x000B6B4E
		protected SmtpMessageContextBlob(bool acceptBlobFromSmptIn, bool sendToSmtpOut, ProcessTransportRole role)
		{
			this.acceptBlobFromSmptIn = acceptBlobFromSmptIn;
			this.sendToSmtpOut = sendToSmtpOut;
			this.processTransportRole = role;
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000B896B File Offset: 0x000B6B6B
		public static AdrcSmtpMessageContextBlob AdrcSmtpMessageContextBlobInstance
		{
			get
			{
				if (SmtpMessageContextBlob.adrcSmtpMessageContextBlobInstance == null)
				{
					SmtpMessageContextBlob.adrcSmtpMessageContextBlobInstance = new AdrcSmtpMessageContextBlob(Components.TransportAppConfig.MessageContextBlobConfiguration.AdvertiseADRecipientCache, Components.TransportAppConfig.MessageContextBlobConfiguration.TransferADRecipientCache, Components.Configuration.ProcessTransportRole);
				}
				return SmtpMessageContextBlob.adrcSmtpMessageContextBlobInstance;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000B89AB File Offset: 0x000B6BAB
		public static ExtendedPropertiesSmtpMessageContextBlob ExtendedPropertiesSmtpMessageContextBlobInstance
		{
			get
			{
				if (SmtpMessageContextBlob.extendedPropertiesSmtpMessageContextBlobInstance == null)
				{
					SmtpMessageContextBlob.extendedPropertiesSmtpMessageContextBlobInstance = new ExtendedPropertiesSmtpMessageContextBlob(Components.TransportAppConfig.MessageContextBlobConfiguration.AdvertiseExtendedProperties, Components.TransportAppConfig.MessageContextBlobConfiguration.TransferExtendedProperties, Components.Configuration.ProcessTransportRole);
				}
				return SmtpMessageContextBlob.extendedPropertiesSmtpMessageContextBlobInstance;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000B89EB File Offset: 0x000B6BEB
		public static FastIndexSmtpMessageContextBlob FastIndexSmtpMessageContextBlobInstance
		{
			get
			{
				if (SmtpMessageContextBlob.fastIndexSmtpMessageContextBlobInstance == null)
				{
					SmtpMessageContextBlob.fastIndexSmtpMessageContextBlobInstance = new FastIndexSmtpMessageContextBlob(Components.TransportAppConfig.MessageContextBlobConfiguration.AdvertiseFastIndex, Components.TransportAppConfig.MessageContextBlobConfiguration.TransferFastIndex, Components.Configuration.ProcessTransportRole);
				}
				return SmtpMessageContextBlob.fastIndexSmtpMessageContextBlobInstance;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x000B8A2B File Offset: 0x000B6C2B
		public virtual bool IsMandatory
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06002E20 RID: 11808
		public abstract string Name { get; }

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06002E21 RID: 11809
		public abstract ByteQuantifiedSize MaxBlobSize { get; }

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000B8A2E File Offset: 0x000B6C2E
		public bool AcceptBlobFromSmptIn
		{
			get
			{
				return this.acceptBlobFromSmptIn;
			}
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000B8A38 File Offset: 0x000B6C38
		public static List<SmtpMessageContextBlob> GetBlobsToSend(IEhloOptions ehloOptions, SmtpOutSession smtpOutSession)
		{
			List<SmtpMessageContextBlob> list = null;
			foreach (SmtpMessageContextBlob smtpMessageContextBlob in SmtpMessageContextBlob.GetSupportedBlobs())
			{
				if (smtpMessageContextBlob.sendToSmtpOut && smtpMessageContextBlob.IsAdvertised(ehloOptions) && smtpMessageContextBlob.HasDataToSend(smtpOutSession) && smtpMessageContextBlob.AllowNextHopType(smtpOutSession))
				{
					if (list == null)
					{
						list = new List<SmtpMessageContextBlob>();
					}
					list.Add(smtpMessageContextBlob);
				}
			}
			return list;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000B8ABC File Offset: 0x000B6CBC
		protected virtual bool AllowNextHopType(SmtpOutSession smtpOutSession)
		{
			return true;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000B8AC0 File Offset: 0x000B6CC0
		public static bool TryGetOrderedListOfBlobsToReceive(string mailCommandParameter, out MailCommandMessageContextParameters messageContextInfo)
		{
			messageContextInfo = null;
			Match match = SmtpMessageContextBlob.MessageContextMailFromRegex.Match(mailCommandParameter);
			if (!match.Success)
			{
				return false;
			}
			List<IInboundMessageContextBlob> list = new List<IInboundMessageContextBlob>(3);
			Version adrc = null;
			Version eprop = null;
			Version fastIndex = null;
			foreach (object obj in match.Groups["Blobs"].Captures)
			{
				Capture capture = (Capture)obj;
				if (capture.Value.StartsWith(AdrcSmtpMessageContextBlob.ADRCBlobName, true, CultureInfo.InvariantCulture))
				{
					if (list.Contains(SmtpMessageContextBlob.AdrcSmtpMessageContextBlobInstance))
					{
						return false;
					}
					if (!AdrcSmtpMessageContextBlob.IsSupportedVersion(capture.Value, true, out adrc))
					{
						return false;
					}
					list.Add(SmtpMessageContextBlob.AdrcSmtpMessageContextBlobInstance);
				}
				else if (capture.Value.StartsWith(ExtendedPropertiesSmtpMessageContextBlob.ExtendedPropertiesBlobName, true, CultureInfo.InvariantCulture))
				{
					if (list.Contains(SmtpMessageContextBlob.ExtendedPropertiesSmtpMessageContextBlobInstance))
					{
						return false;
					}
					if (!ExtendedPropertiesSmtpMessageContextBlob.IsSupportedVersion(capture.Value, out eprop))
					{
						return false;
					}
					list.Add(SmtpMessageContextBlob.ExtendedPropertiesSmtpMessageContextBlobInstance);
				}
				else
				{
					if (!capture.Value.StartsWith(FastIndexSmtpMessageContextBlob.FastIndexBlobName, true, CultureInfo.InvariantCulture))
					{
						throw new InvalidOperationException("Unexpected " + capture.Value);
					}
					if (list.Contains(SmtpMessageContextBlob.FastIndexSmtpMessageContextBlobInstance))
					{
						return false;
					}
					if (!FastIndexSmtpMessageContextBlob.IsSupportedVersion(capture.Value, out fastIndex))
					{
						return false;
					}
					list.Add(SmtpMessageContextBlob.FastIndexSmtpMessageContextBlobInstance);
				}
			}
			messageContextInfo = new MailCommandMessageContextParameters(mailCommandParameter, adrc, eprop, fastIndex, list);
			return true;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000B8C84 File Offset: 0x000B6E84
		public static List<SmtpMessageContextBlob> GetAdvertisedMandatoryBlobs(IEhloOptions ehloOptions)
		{
			List<SmtpMessageContextBlob> list = new List<SmtpMessageContextBlob>(1);
			foreach (SmtpMessageContextBlob smtpMessageContextBlob in SmtpMessageContextBlob.GetSupportedBlobs())
			{
				if (smtpMessageContextBlob.IsMandatory && smtpMessageContextBlob.IsAdvertised(ehloOptions))
				{
					list.Add(smtpMessageContextBlob);
				}
			}
			return list;
		}

		// Token: 0x06002E27 RID: 11815
		public abstract bool IsAdvertised(IEhloOptions ehloOptions);

		// Token: 0x06002E28 RID: 11816
		public abstract void DeserializeBlob(Stream stream, ISmtpInSession smtpInSession, long blobSize);

		// Token: 0x06002E29 RID: 11817
		public abstract void DeserializeBlob(Stream stream, SmtpInSessionState sessionState, long blobSize);

		// Token: 0x06002E2A RID: 11818
		public abstract Stream SerializeBlob(SmtpOutSession smtpOutSession);

		// Token: 0x06002E2B RID: 11819
		public abstract bool VerifyPermission(Permission permission);

		// Token: 0x06002E2C RID: 11820 RVA: 0x000B8CF0 File Offset: 0x000B6EF0
		public virtual bool HasDataToSend(SmtpOutSession smtpOutSession)
		{
			return true;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000B8CF3 File Offset: 0x000B6EF3
		public virtual string GetVersionToSend(IEhloOptions ehloOptions)
		{
			return this.Name;
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000B8CFB File Offset: 0x000B6EFB
		protected static int ReadInt(Stream stream, byte[] intValueReadBuffer)
		{
			stream.Read(intValueReadBuffer, 0, 4);
			return BitConverter.ToInt32(intValueReadBuffer, 0);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000B8D10 File Offset: 0x000B6F10
		protected T ReadTypeAndValidate<T>(TransportPropertyStreamReader reader)
		{
			object obj = reader.ReadValue();
			if (obj == null || obj.GetType() != typeof(T))
			{
				string text = (obj == null) ? "<null>" : obj.GetType().ToString();
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Encountered unexpected value {0} while deserializing. Expected type was {1}", new object[]
				{
					text,
					typeof(T)
				}));
			}
			return (T)((object)obj);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000B8D88 File Offset: 0x000B6F88
		protected void SerializeValue(Stream ms, ref byte[] buffer, object value)
		{
			StreamPropertyType propetyTypeForValue = this.GetPropetyTypeForValue(value);
			int size = TransportPropertyStreamWriter.SizeOf(propetyTypeForValue, value);
			buffer = this.IncreaseBufferIfSmall(size, buffer);
			int count = 0;
			TransportPropertyStreamWriter.Serialize(propetyTypeForValue, value, buffer, ref count);
			ms.Write(buffer, 0, count);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000B8DC8 File Offset: 0x000B6FC8
		protected bool IsNativelySupported(ADPropertyDefinition propertyDefinition)
		{
			Type type = propertyDefinition.Type;
			if (!propertyDefinition.IsMultivalued)
			{
				return SmtpMessageContextBlob.supportedTypes.ContainsKey(type);
			}
			return SmtpMessageContextBlob.multiValuedSupportedTypes.Contains(type);
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000B8DFC File Offset: 0x000B6FFC
		protected StreamPropertyType GetPropetyTypeForValue(object value)
		{
			if (value == null)
			{
				return StreamPropertyType.Null;
			}
			StreamPropertyType result;
			if (SmtpMessageContextBlob.supportedTypes.TryGetValue(value.GetType(), out result))
			{
				return result;
			}
			if (typeof(ProxyAddress).IsAssignableFrom(value.GetType()))
			{
				return StreamPropertyType.ProxyAddress;
			}
			throw new InvalidOperationException(string.Format("Do not know how to serialize type {0}. If this is a new type added to ADRecipientCache, make sure it is convertable by ADValueConvertor or add support for the type in PropertWriter/PropertyReader. Also remember to increment the version if needed.", value.GetType()));
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000B8E53 File Offset: 0x000B7053
		protected byte[] IncreaseBufferIfSmall(int size, byte[] buffer)
		{
			if (size > buffer.Length)
			{
				buffer = new byte[size * 2];
			}
			return buffer;
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000B8E68 File Offset: 0x000B7068
		private static List<SmtpMessageContextBlob> GetSupportedBlobs()
		{
			if (SmtpMessageContextBlob.supportedBlobs == null)
			{
				SmtpMessageContextBlob.supportedBlobs = new List<SmtpMessageContextBlob>
				{
					SmtpMessageContextBlob.AdrcSmtpMessageContextBlobInstance,
					SmtpMessageContextBlob.ExtendedPropertiesSmtpMessageContextBlobInstance,
					SmtpMessageContextBlob.FastIndexSmtpMessageContextBlobInstance
				};
			}
			return SmtpMessageContextBlob.supportedBlobs;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000B8EB0 File Offset: 0x000B70B0
		private static HashSet<Type> GetMultiValuedSupportedTypes()
		{
			return new HashSet<Type>
			{
				typeof(bool),
				typeof(byte),
				typeof(sbyte),
				typeof(short),
				typeof(ushort),
				typeof(int),
				typeof(uint),
				typeof(long),
				typeof(ulong),
				typeof(float),
				typeof(double),
				typeof(decimal),
				typeof(char),
				typeof(string),
				typeof(DateTime),
				typeof(Guid),
				typeof(IPAddress),
				typeof(IPEndPoint),
				typeof(RoutingAddress),
				typeof(ADObjectId),
				typeof(ADObjectIdWithString),
				typeof(byte[]),
				typeof(ProxyAddress)
			};
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000B904C File Offset: 0x000B724C
		private static Dictionary<Type, StreamPropertyType> GetSupportedTypes()
		{
			TypeEntry[] array = new TypeEntry[]
			{
				new TypeEntry(typeof(DBNull), StreamPropertyType.Null),
				new TypeEntry(typeof(bool), StreamPropertyType.Bool),
				new TypeEntry(typeof(byte), StreamPropertyType.Byte),
				new TypeEntry(typeof(sbyte), StreamPropertyType.SByte),
				new TypeEntry(typeof(short), StreamPropertyType.Int16),
				new TypeEntry(typeof(ushort), StreamPropertyType.UInt16),
				new TypeEntry(typeof(int), StreamPropertyType.Int32),
				new TypeEntry(typeof(uint), StreamPropertyType.UInt32),
				new TypeEntry(typeof(long), StreamPropertyType.Int64),
				new TypeEntry(typeof(ulong), StreamPropertyType.UInt64),
				new TypeEntry(typeof(float), StreamPropertyType.Single),
				new TypeEntry(typeof(double), StreamPropertyType.Double),
				new TypeEntry(typeof(decimal), StreamPropertyType.Decimal),
				new TypeEntry(typeof(char), StreamPropertyType.Char),
				new TypeEntry(typeof(string), StreamPropertyType.String),
				new TypeEntry(typeof(DateTime), StreamPropertyType.DateTime),
				new TypeEntry(typeof(Guid), StreamPropertyType.Guid),
				new TypeEntry(typeof(IPAddress), StreamPropertyType.IPAddress),
				new TypeEntry(typeof(IPEndPoint), StreamPropertyType.IPEndPoint),
				new TypeEntry(typeof(RoutingAddress), StreamPropertyType.RoutingAddress),
				new TypeEntry(typeof(ADObjectId), StreamPropertyType.ADObjectIdUTF8),
				new TypeEntry(typeof(Microsoft.Exchange.Data.Directory.Recipient.RecipientType), StreamPropertyType.RecipientType),
				new TypeEntry(typeof(ADObjectIdWithString), StreamPropertyType.ADObjectIdWithString),
				new TypeEntry(typeof(bool[]), StreamPropertyType.Bool | StreamPropertyType.Array),
				new TypeEntry(typeof(byte[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.Array),
				new TypeEntry(typeof(sbyte[]), StreamPropertyType.SByte | StreamPropertyType.Array),
				new TypeEntry(typeof(short[]), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.Array),
				new TypeEntry(typeof(ushort[]), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.Array),
				new TypeEntry(typeof(int[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.Array),
				new TypeEntry(typeof(uint[]), StreamPropertyType.UInt32 | StreamPropertyType.Array),
				new TypeEntry(typeof(long[]), StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.Array),
				new TypeEntry(typeof(ulong[]), StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.Array),
				new TypeEntry(typeof(float[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.Array),
				new TypeEntry(typeof(double[]), StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array),
				new TypeEntry(typeof(decimal[]), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array),
				new TypeEntry(typeof(char[]), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array),
				new TypeEntry(typeof(string[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array),
				new TypeEntry(typeof(DateTime[]), StreamPropertyType.DateTime | StreamPropertyType.Array),
				new TypeEntry(typeof(Guid[]), StreamPropertyType.Null | StreamPropertyType.DateTime | StreamPropertyType.Array),
				new TypeEntry(typeof(IPAddress[]), StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.Array),
				new TypeEntry(typeof(IPEndPoint[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.Array),
				new TypeEntry(typeof(RoutingAddress[]), StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.Array),
				new TypeEntry(typeof(ADObjectId[]), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.Array),
				new TypeEntry(typeof(ADObjectIdWithString[]), StreamPropertyType.UInt32 | StreamPropertyType.DateTime | StreamPropertyType.Array),
				new TypeEntry(typeof(List<bool>), StreamPropertyType.Bool | StreamPropertyType.List),
				new TypeEntry(typeof(List<byte>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.List),
				new TypeEntry(typeof(List<sbyte>), StreamPropertyType.SByte | StreamPropertyType.List),
				new TypeEntry(typeof(List<short>), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.List),
				new TypeEntry(typeof(List<ushort>), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.List),
				new TypeEntry(typeof(List<int>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.List),
				new TypeEntry(typeof(List<uint>), StreamPropertyType.UInt32 | StreamPropertyType.List),
				new TypeEntry(typeof(List<long>), StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.List),
				new TypeEntry(typeof(List<ulong>), StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.List),
				new TypeEntry(typeof(List<float>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.List),
				new TypeEntry(typeof(List<double>), StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List),
				new TypeEntry(typeof(List<decimal>), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List),
				new TypeEntry(typeof(List<char>), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List),
				new TypeEntry(typeof(List<string>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.List),
				new TypeEntry(typeof(List<DateTime>), StreamPropertyType.DateTime | StreamPropertyType.List),
				new TypeEntry(typeof(List<Guid>), StreamPropertyType.Null | StreamPropertyType.DateTime | StreamPropertyType.List),
				new TypeEntry(typeof(List<IPAddress>), StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.List),
				new TypeEntry(typeof(List<IPEndPoint>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.List),
				new TypeEntry(typeof(List<RoutingAddress>), StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.List),
				new TypeEntry(typeof(List<ADObjectId>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.List),
				new TypeEntry(typeof(List<ADObjectIdWithString>), StreamPropertyType.UInt32 | StreamPropertyType.DateTime | StreamPropertyType.List),
				new TypeEntry(typeof(List<byte[]>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.Array | StreamPropertyType.List),
				new TypeEntry(typeof(MultiValuedProperty<bool>), StreamPropertyType.Bool | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<byte>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<sbyte>), StreamPropertyType.SByte | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<short>), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<ushort>), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<int>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<uint>), StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<long>), StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<ulong>), StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<float>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<double>), StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<decimal>), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<char>), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<string>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<DateTime>), StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<Guid>), StreamPropertyType.Null | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<IPAddress>), StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<IPEndPoint>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<RoutingAddress>), StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<ADObjectId>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<ADObjectIdWithString>), StreamPropertyType.UInt32 | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(MultiValuedProperty<byte[]>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.Array | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ProxyAddressCollection), StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<bool>), StreamPropertyType.Bool | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<byte>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<sbyte>), StreamPropertyType.SByte | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<short>), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<ushort>), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<int>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<uint>), StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<long>), StreamPropertyType.Null | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<ulong>), StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<float>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<double>), StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<decimal>), StreamPropertyType.Null | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<char>), StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<string>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<DateTime>), StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<Guid>), StreamPropertyType.Null | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<IPAddress>), StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<IPEndPoint>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<RoutingAddress>), StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<ADObjectId>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<ADObjectIdWithString>), StreamPropertyType.UInt32 | StreamPropertyType.DateTime | StreamPropertyType.MultiValuedProperty),
				new TypeEntry(typeof(ADMultiValuedProperty<byte[]>), StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.Array | StreamPropertyType.MultiValuedProperty)
			};
			Dictionary<Type, StreamPropertyType> dictionary = new Dictionary<Type, StreamPropertyType>(array.Length);
			foreach (TypeEntry typeEntry in array)
			{
				dictionary.Add(typeEntry.Type, typeEntry.Identifier);
			}
			return dictionary;
		}

		// Token: 0x040016D2 RID: 5842
		public static readonly string ProcessTransportRoleKey = "Microsoft.Exchange.Protocols.Smtp.SmtpMessageContextBlob.ProcessTransportRole";

		// Token: 0x040016D3 RID: 5843
		private static readonly Regex MessageContextMailFromRegex = new Regex("^((?<Blobs>(ADRC|EPROP|FSTINDX)-\\d{1,5}\\.\\d{1,7}\\.\\d{1,7}\\.\\d{1,7})(,)?){1,20}$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x040016D4 RID: 5844
		private static AdrcSmtpMessageContextBlob adrcSmtpMessageContextBlobInstance;

		// Token: 0x040016D5 RID: 5845
		private static ExtendedPropertiesSmtpMessageContextBlob extendedPropertiesSmtpMessageContextBlobInstance;

		// Token: 0x040016D6 RID: 5846
		private static FastIndexSmtpMessageContextBlob fastIndexSmtpMessageContextBlobInstance;

		// Token: 0x040016D7 RID: 5847
		private static List<SmtpMessageContextBlob> supportedBlobs;

		// Token: 0x040016D8 RID: 5848
		private static Dictionary<Type, StreamPropertyType> supportedTypes = SmtpMessageContextBlob.GetSupportedTypes();

		// Token: 0x040016D9 RID: 5849
		private static HashSet<Type> multiValuedSupportedTypes = SmtpMessageContextBlob.GetMultiValuedSupportedTypes();

		// Token: 0x040016DA RID: 5850
		private readonly bool acceptBlobFromSmptIn;

		// Token: 0x040016DB RID: 5851
		private readonly bool sendToSmtpOut;

		// Token: 0x040016DC RID: 5852
		protected readonly ProcessTransportRole processTransportRole;
	}
}
