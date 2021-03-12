using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200006E RID: 110
	public struct MimeParameterReader
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x00019077 File Offset: 0x00017277
		internal MimeParameterReader(MimeReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00019080 File Offset: 0x00017280
		public string Name
		{
			get
			{
				this.AssertGood(true);
				return this.reader.ReadParameterName();
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00019094 File Offset: 0x00017294
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

		// Token: 0x0600044D RID: 1101 RVA: 0x000190C0 File Offset: 0x000172C0
		public bool ReadNextParameter()
		{
			this.AssertGood(false);
			return this.reader.ReadNextDescendant(true);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000190D8 File Offset: 0x000172D8
		public bool TryGetValue(out string value)
		{
			DecodingResults decodingResults;
			return this.TryGetValue(this.reader.HeaderDecodingOptions, out decodingResults, out value);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000190F9 File Offset: 0x000172F9
		public bool TryGetValue(DecodingOptions decodingOptions, out DecodingResults decodingResults, out string value)
		{
			this.AssertGood(true);
			return this.reader.TryReadParameterValue(decodingOptions, out decodingResults, out value);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00019110 File Offset: 0x00017310
		private void AssertGood(bool checkPositionedOnParameter)
		{
			if (this.reader == null)
			{
				throw new NotSupportedException(Strings.ParameterReaderNotInitialized);
			}
			this.reader.AssertGoodToUse(true, true);
			if (this.reader.ReaderState != MimeReaderState.HeaderComplete || this.reader.CurrentHeaderObject == null || !(this.reader.CurrentHeaderObject is ComplexHeader))
			{
				throw new NotSupportedException(Strings.ReaderIsNotPositionedOnHeaderWithParameters);
			}
			if (checkPositionedOnParameter && !this.reader.IsCurrentChildValid(true))
			{
				throw new InvalidOperationException(Strings.ParameterReaderIsNotPositionedOnParameter);
			}
		}

		// Token: 0x0400032F RID: 815
		private MimeReader reader;
	}
}
