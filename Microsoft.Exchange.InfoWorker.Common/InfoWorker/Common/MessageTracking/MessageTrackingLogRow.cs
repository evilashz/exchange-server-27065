using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.LogSearch;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002CE RID: 718
	internal sealed class MessageTrackingLogRow
	{
		// Token: 0x06001445 RID: 5189 RVA: 0x0005F040 File Offset: 0x0005D240
		private static MessageTrackingLogRow.ValidateMethod[] CreateValidateMethods()
		{
			MessageTrackingLogRow.ValidateMethod[] array = new MessageTrackingLogRow.ValidateMethod[MessageTrackingLogRow.FieldCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = null;
			}
			array[8] = new MessageTrackingLogRow.ValidateMethod(MessageTrackingLogRow.EnumerationValidator<MessageTrackingEvent>);
			array[7] = new MessageTrackingLogRow.ValidateMethod(MessageTrackingLogRow.EnumerationValidator<MessageTrackingSource>);
			array[9] = new MessageTrackingLogRow.ValidateMethod(MessageTrackingLogRow.InternalMessageIdValidator);
			array[12] = new MessageTrackingLogRow.ValidateMethod(MessageTrackingLogRow.SmtpAddressArrayValidator);
			array[19] = new MessageTrackingLogRow.ValidateMethod(MessageTrackingLogRow.SmtpAddressValidator);
			array[20] = null;
			return array;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0005F0C0 File Offset: 0x0005D2C0
		private static bool DateTimeValidator(object value)
		{
			DateTime dateTime;
			return DateTime.TryParse((string)value, out dateTime);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0005F0DA File Offset: 0x0005D2DA
		private static bool SmtpAddressValidator(object value)
		{
			return SmtpAddress.IsValidSmtpAddress((string)value);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0005F0E7 File Offset: 0x0005D2E7
		private static bool SmtpAddressValidatorWithNull(object value)
		{
			return string.IsNullOrEmpty((string)value) || MessageTrackingLogRow.SmtpAddressValidator(value);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0005F100 File Offset: 0x0005D300
		private static bool EnumerationValidator<T>(object value) where T : struct
		{
			T t;
			return value != null && EnumValidator<T>.TryParse((string)value, EnumParseOptions.Default, out t);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0005F120 File Offset: 0x0005D320
		private static bool InternalMessageIdValidator(object value)
		{
			long num;
			return string.IsNullOrEmpty((string)value) || long.TryParse((string)value, out num);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0005F14C File Offset: 0x0005D34C
		private static bool SmtpAddressArrayValidator(object value)
		{
			if (value == null)
			{
				return true;
			}
			string[] array = (string[])value;
			if (array.Length == 0 || (array.Length == 1 && string.IsNullOrEmpty(array[0])))
			{
				return true;
			}
			foreach (string value2 in array)
			{
				if (!MessageTrackingLogRow.SmtpAddressValidator(value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0005F1A4 File Offset: 0x0005D3A4
		private static bool ProxyAddressArrayValidator(object value)
		{
			string[] array = (string[])value;
			foreach (string value2 in array)
			{
				if (!MessageTrackingLogRow.ProxyAddressValidator(value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0005F1E0 File Offset: 0x0005D3E0
		private static bool ProxyAddressValidator(object value)
		{
			ProxyAddress proxyAddress = null;
			return ProxyAddress.TryParse(value as string, out proxyAddress);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0005F1FC File Offset: 0x0005D3FC
		private static bool ValidateField(MessageTrackingLogRow logEntry, int fieldIdx, object value, TrackingErrorCollection errors)
		{
			MessageTrackingLogRow.ValidateMethod validator = MessageTrackingLogRow.GetValidator(logEntry, fieldIdx);
			if (validator != null && !validator(value))
			{
				string text = string.Format("The message-tracking data on server {0} had invalid data in column {1} for this message. Event \"{2}\" will be ignored", logEntry.serverFqdn, fieldIdx, logEntry.EventId.ToString());
				errors.Add(ErrorCode.UnexpectedErrorPermanent, logEntry.ServerHostName, text, string.Empty);
				TraceWrapper.SearchLibraryTracer.TraceError(logEntry.GetHashCode(), text, new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0005F274 File Offset: 0x0005D474
		private static MessageTrackingLogRow.ValidateMethod GetValidator(MessageTrackingLogRow logEntry, int fieldIdx)
		{
			if (!logEntry.IsValidColumn(MessageTrackingField.EventId))
			{
				return null;
			}
			if (logEntry.EventId == MessageTrackingEvent.THROTTLE && fieldIdx == 12)
			{
				return new MessageTrackingLogRow.ValidateMethod(MessageTrackingLogRow.ProxyAddressArrayValidator);
			}
			if (logEntry.EventId == MessageTrackingEvent.DEFER && logEntry.Source == MessageTrackingSource.SMTP && fieldIdx == 19)
			{
				return null;
			}
			return MessageTrackingLogRow.validateMethods[fieldIdx];
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0005F2C8 File Offset: 0x0005D4C8
		private static string[] CanonicalizeStringArray(string[] value)
		{
			if (value == null || value.Length == 0 || (value.Length == 1 && string.IsNullOrEmpty(value[0])))
			{
				return null;
			}
			return value;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0005F2E5 File Offset: 0x0005D4E5
		private MessageTrackingLogRow()
		{
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0005F30D File Offset: 0x0005D50D
		private T GetColumn<T>(MessageTrackingField idx)
		{
			if (!this.validColumns[(int)idx])
			{
				throw new InvalidOperationException();
			}
			return (T)((object)this.columns[(int)idx]);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0005F330 File Offset: 0x0005D530
		public static bool TryRead(string server, LogSearchCursor cursor, BitArray fieldsToGet, TrackingErrorCollection errors, out MessageTrackingLogRow logEntry)
		{
			logEntry = null;
			MessageTrackingLogRow messageTrackingLogRow = new MessageTrackingLogRow();
			messageTrackingLogRow.serverFqdn = server;
			for (int i = 0; i < fieldsToGet.Length; i++)
			{
				if (fieldsToGet[i])
				{
					object obj = null;
					Exception ex = null;
					try
					{
						obj = cursor.GetField(i);
					}
					catch (LogSearchException ex2)
					{
						ex = ex2;
						int errorCode = ex2.ErrorCode;
					}
					catch (RpcException ex3)
					{
						ex = ex3;
						int errorCode2 = ex3.ErrorCode;
					}
					if (ex != null)
					{
						TrackingTransientException.AddAndRaiseETX(errors, ErrorCode.LogSearchConnection, server, ex.ToString());
					}
					if (obj != null && obj.GetType() != MessageTrackingSchema.MessageTrackingEvent.Fields[i].Type)
					{
						throw new InvalidOperationException(string.Format("Schema for column {0} in message-tracking is of sync with tasks", i));
					}
					if (!MessageTrackingLogRow.ValidateField(messageTrackingLogRow, i, obj, errors))
					{
						return false;
					}
					messageTrackingLogRow.columns[i] = cursor.GetField(i);
					messageTrackingLogRow.validColumns[i] = true;
				}
			}
			if (fieldsToGet[8] && (messageTrackingLogRow.EventId == MessageTrackingEvent.EXPAND || messageTrackingLogRow.EventId == MessageTrackingEvent.RESOLVE))
			{
				MessageTrackingLogRow.SmtpAddressValidatorWithNull(messageTrackingLogRow.RelatedRecipientAddress);
			}
			logEntry = messageTrackingLogRow;
			return true;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0005F458 File Offset: 0x0005D658
		public static BitArray GetColumnFilter(params MessageTrackingField[] columns)
		{
			BitArray bitArray = new BitArray(MessageTrackingLogRow.FieldCount);
			foreach (MessageTrackingField index in columns)
			{
				bitArray[(int)index] = true;
			}
			return bitArray;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0005F48D File Offset: 0x0005D68D
		public bool IsValidColumn(MessageTrackingField column)
		{
			return this.validColumns[(int)column];
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0005F49B File Offset: 0x0005D69B
		public DateTime DateTime
		{
			get
			{
				return this.GetColumn<DateTime>(MessageTrackingField.Timestamp);
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0005F4A4 File Offset: 0x0005D6A4
		public string MessageId
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.MessageId);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0005F4AE File Offset: 0x0005D6AE
		public string ClientHostName
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.ClientHostname);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0005F4B7 File Offset: 0x0005D6B7
		public string ClientIP
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.ClientIp);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0005F4C0 File Offset: 0x0005D6C0
		public long InternalMessageId
		{
			get
			{
				string column = this.GetColumn<string>(MessageTrackingField.InternalMessageId);
				if (string.IsNullOrEmpty(column))
				{
					return 0L;
				}
				return long.Parse(column);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0005F4E7 File Offset: 0x0005D6E7
		public string SenderAddress
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.SenderAddress);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0005F4F1 File Offset: 0x0005D6F1
		public string[] RecipientAddresses
		{
			get
			{
				return MessageTrackingLogRow.CanonicalizeStringArray(this.GetColumn<string[]>(MessageTrackingField.RecipientAddress));
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0005F500 File Offset: 0x0005D700
		public string[] RecipientStatuses
		{
			get
			{
				return MessageTrackingLogRow.CanonicalizeStringArray(this.GetColumn<string[]>(MessageTrackingField.RecipientStatus));
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0005F50F File Offset: 0x0005D70F
		public string MessageSubject
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.MessageSubject);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0005F51C File Offset: 0x0005D71C
		public MessageTrackingEvent EventId
		{
			get
			{
				string column = this.GetColumn<string>(MessageTrackingField.EventId);
				return EnumValidator<MessageTrackingEvent>.Parse(column, EnumParseOptions.Default);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0005F538 File Offset: 0x0005D738
		public MessageTrackingSource Source
		{
			get
			{
				string column = this.GetColumn<string>(MessageTrackingField.Source);
				return EnumValidator<MessageTrackingSource>.Parse(column, EnumParseOptions.Default);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0005F554 File Offset: 0x0005D754
		public string ServerHostName
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.ServerHostname);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0005F55D File Offset: 0x0005D75D
		public string ServerIP
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.ServerIp);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0005F566 File Offset: 0x0005D766
		public string[] References
		{
			get
			{
				return this.GetColumn<string[]>(MessageTrackingField.Reference);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0005F570 File Offset: 0x0005D770
		public string RelatedRecipientAddress
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.RelatedRecipientAddress);
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0005F57A File Offset: 0x0005D77A
		public string Context
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.SourceContext);
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0005F583 File Offset: 0x0005D783
		public string ServerFqdn
		{
			get
			{
				return this.serverFqdn;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0005F58B File Offset: 0x0005D78B
		public KeyValuePair<string, object>[] CustomData
		{
			get
			{
				return this.GetColumn<KeyValuePair<string, object>[]>(MessageTrackingField.CustomData);
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0005F595 File Offset: 0x0005D795
		public string TenantId
		{
			get
			{
				return this.GetColumn<string>(MessageTrackingField.TenantId);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0005F5A0 File Offset: 0x0005D7A0
		public bool IsLogCompatible
		{
			get
			{
				if (this.isLogCompatible == null)
				{
					string customData = this.GetCustomData<string>("Version", string.Empty);
					this.isLogCompatible = new bool?(string.IsNullOrEmpty(customData));
				}
				return this.isLogCompatible.Value;
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
		public T GetCustomData<T>(string key, T defaultValue)
		{
			KeyValuePair<string, object>[] customData = this.CustomData;
			if (customData == null || customData.Length == 0)
			{
				return defaultValue;
			}
			foreach (KeyValuePair<string, object> keyValuePair in customData)
			{
				if (string.Equals(keyValuePair.Key, key, StringComparison.OrdinalIgnoreCase))
				{
					object value = keyValuePair.Value;
					if (!(value is T))
					{
						TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "The message-tracking data on server {0} had invalid data in the CustomData column for this message", new object[]
						{
							this.ServerFqdn
						});
					}
					return (T)((object)value);
				}
			}
			return defaultValue;
		}

		// Token: 0x04000D51 RID: 3409
		public const string VersionPropertyName = "Version";

		// Token: 0x04000D52 RID: 3410
		public static readonly int FieldCount = MessageTrackingSchema.MessageTrackingEvent.Fields.Length;

		// Token: 0x04000D53 RID: 3411
		private static MessageTrackingLogRow.ValidateMethod[] validateMethods = MessageTrackingLogRow.CreateValidateMethods();

		// Token: 0x04000D54 RID: 3412
		private object[] columns = new object[MessageTrackingLogRow.FieldCount];

		// Token: 0x04000D55 RID: 3413
		private BitArray validColumns = new BitArray(MessageTrackingLogRow.FieldCount);

		// Token: 0x04000D56 RID: 3414
		private string serverFqdn;

		// Token: 0x04000D57 RID: 3415
		private bool? isLogCompatible;

		// Token: 0x020002CF RID: 719
		// (Invoke) Token: 0x0600146D RID: 5229
		private delegate bool ValidateMethod(object value);
	}
}
