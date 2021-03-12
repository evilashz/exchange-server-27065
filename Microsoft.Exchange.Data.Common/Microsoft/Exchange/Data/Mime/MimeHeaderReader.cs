using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200006C RID: 108
	public struct MimeHeaderReader
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x00018BE0 File Offset: 0x00016DE0
		internal MimeHeaderReader(MimeReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00018BE9 File Offset: 0x00016DE9
		internal MimeReader MimeReader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00018BF1 File Offset: 0x00016DF1
		public HeaderId HeaderId
		{
			get
			{
				this.AssertGood(true);
				return this.reader.HeaderId;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00018C05 File Offset: 0x00016E05
		public string Name
		{
			get
			{
				this.AssertGood(true);
				return this.reader.HeaderName;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00018C19 File Offset: 0x00016E19
		public bool IsAddressHeader
		{
			get
			{
				this.AssertGood(true);
				return Header.TypeFromHeaderId(this.HeaderId) == typeof(AddressHeader);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00018C3C File Offset: 0x00016E3C
		public MimeAddressReader AddressReader
		{
			get
			{
				if (!this.IsAddressHeader)
				{
					throw new InvalidOperationException(Strings.HeaderCannotHaveAddresses);
				}
				if (this.reader.ReaderState == MimeReaderState.HeaderStart)
				{
					this.reader.TryCompleteCurrentHeader(true);
				}
				return new MimeAddressReader(this.reader, true);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00018C78 File Offset: 0x00016E78
		public bool CanHaveParameters
		{
			get
			{
				this.AssertGood(true);
				Type left = Header.TypeFromHeaderId(this.HeaderId);
				return left == typeof(ContentTypeHeader) || left == typeof(ContentDispositionHeader);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00018CBC File Offset: 0x00016EBC
		public MimeParameterReader ParameterReader
		{
			get
			{
				if (!this.CanHaveParameters)
				{
					throw new InvalidOperationException(Strings.HeaderCannotHaveParameters);
				}
				if (this.reader.ReaderState == MimeReaderState.HeaderStart)
				{
					this.reader.TryCompleteCurrentHeader(true);
				}
				return new MimeParameterReader(this.reader);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00018CF8 File Offset: 0x00016EF8
		public string Value
		{
			get
			{
				DecodingResults decodingResults;
				string result;
				if (!this.TryGetValue(this.reader.HeaderDecodingOptions, out decodingResults, out result))
				{
					MimeCommon.ThrowDecodingFailedException(ref decodingResults);
				}
				return result;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00018D24 File Offset: 0x00016F24
		public DateTime ReadValueAsDateTime()
		{
			this.AssertGood(true);
			if (this.reader.ReaderState == MimeReaderState.HeaderStart)
			{
				this.reader.TryCompleteCurrentHeader(true);
			}
			if (this.reader.CurrentHeaderObject == null)
			{
				return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
			}
			DateHeader dateHeader = this.reader.CurrentHeaderObject as DateHeader;
			if (dateHeader != null)
			{
				return dateHeader.DateTime;
			}
			return DateHeader.ParseDateHeaderValue(this.reader.CurrentHeaderObject.Value);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00018D9C File Offset: 0x00016F9C
		public bool ReadNextHeader()
		{
			this.AssertGood(false);
			while (this.reader.ReadNextHeader())
			{
				if (this.reader.HeaderName != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00018DC4 File Offset: 0x00016FC4
		public bool TryGetValue(out string value)
		{
			DecodingResults decodingResults;
			return this.TryGetValue(this.reader.HeaderDecodingOptions, out decodingResults, out value);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00018DE8 File Offset: 0x00016FE8
		public bool TryGetValue(DecodingOptions decodingOptions, out DecodingResults decodingResults, out string value)
		{
			this.AssertGood(true);
			if (this.reader.ReaderState == MimeReaderState.HeaderStart)
			{
				this.reader.TryCompleteCurrentHeader(true);
			}
			if (this.reader.CurrentHeaderObject != null)
			{
				TextHeader textHeader = this.reader.CurrentHeaderObject as TextHeader;
				if (textHeader != null)
				{
					value = textHeader.GetDecodedValue(decodingOptions, out decodingResults);
					if (decodingResults.DecodingFailed)
					{
						value = null;
						return false;
					}
					return true;
				}
				else
				{
					value = this.reader.CurrentHeaderObject.Value;
				}
			}
			else
			{
				value = null;
			}
			decodingResults = default(DecodingResults);
			return true;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00018E70 File Offset: 0x00017070
		private void AssertGood(bool checkPositionedOnHeader)
		{
			if (this.reader == null)
			{
				throw new NotSupportedException(Strings.HeaderReaderNotInitialized);
			}
			this.reader.AssertGoodToUse(true, true);
			if (!MimeReader.StateIsOneOf(this.reader.ReaderState, MimeReaderState.PartStart | MimeReaderState.HeaderStart | MimeReaderState.HeaderIncomplete | MimeReaderState.HeaderComplete | MimeReaderState.EndOfHeaders | MimeReaderState.InlineStart))
			{
				throw new NotSupportedException(Strings.HeaderReaderCannotBeUsedInThisState);
			}
			if (checkPositionedOnHeader && MimeReader.StateIsOneOf(this.reader.ReaderState, MimeReaderState.PartStart | MimeReaderState.EndOfHeaders))
			{
				throw new InvalidOperationException(Strings.HeaderReaderIsNotPositionedOnAHeader);
			}
		}

		// Token: 0x0400032C RID: 812
		private MimeReader reader;
	}
}
