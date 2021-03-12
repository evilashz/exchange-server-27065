using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000031 RID: 49
	public class MimeGroup : AddressItem, IEnumerable<MimeRecipient>, IEnumerable
	{
		// Token: 0x0600021C RID: 540 RVA: 0x000095FE File Offset: 0x000077FE
		public MimeGroup()
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009606 File Offset: 0x00007806
		public MimeGroup(string displayName) : base(displayName)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000960F File Offset: 0x0000780F
		internal MimeGroup(ref MimeStringList displayName) : base(ref displayName)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009618 File Offset: 0x00007818
		public new MimeNode.Enumerator<MimeRecipient> GetEnumerator()
		{
			return new MimeNode.Enumerator<MimeRecipient>(this);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009620 File Offset: 0x00007820
		IEnumerator<MimeRecipient> IEnumerable<MimeRecipient>.GetEnumerator()
		{
			return new MimeNode.Enumerator<MimeRecipient>(this);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000962D File Offset: 0x0000782D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MimeNode.Enumerator<MimeRecipient>(this);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000963C File Offset: 0x0000783C
		public sealed override MimeNode Clone()
		{
			MimeGroup mimeGroup = new MimeGroup();
			this.CopyTo(mimeGroup);
			return mimeGroup;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009658 File Offset: 0x00007858
		public sealed override void CopyTo(object destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == this)
			{
				return;
			}
			MimeGroup mimeGroup = destination as MimeGroup;
			if (mimeGroup == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			while (mimeGroup.ParseNextChild() != null)
			{
			}
			while (this.ParseNextChild() != null)
			{
			}
			base.CopyTo(destination);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000096A4 File Offset: 0x000078A4
		internal override MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			if (!(newChild is MimeRecipient))
			{
				throw new ArgumentException(Strings.NewChildIsNotARecipient);
			}
			return refChild;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000096BC File Offset: 0x000078BC
		internal override long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			MimeNode nextSibling = base.NextSibling;
			MimeStringList displayNameToWrite = base.GetDisplayNameToWrite(encodingOptions);
			long num = 0L;
			if (displayNameToWrite.GetLength(4026531839U) != 0)
			{
				int num2 = 1;
				if (base.FirstChild == null)
				{
					num2++;
				}
				if (base.NextSibling != null)
				{
					num2++;
				}
				num += Header.QuoteAndFold(stream, displayNameToWrite, 4026531839U, base.IsQuotingRequired(displayNameToWrite, encodingOptions.AllowUTF8), true, encodingOptions.AllowUTF8, num2, ref currentLineLength, ref scratchBuffer);
				stream.Write(MimeString.Colon, 0, MimeString.Colon.Length);
				num += (long)MimeString.Colon.Length;
				currentLineLength.IncrementBy(MimeString.Colon.Length);
			}
			MimeNode mimeNode = base.FirstChild;
			int num3 = 0;
			while (mimeNode != null)
			{
				if (1 < ++num3)
				{
					stream.Write(MimeString.Comma, 0, MimeString.Comma.Length);
					num += (long)MimeString.Comma.Length;
					currentLineLength.IncrementBy(MimeString.Comma.Length);
				}
				num += mimeNode.WriteTo(stream, encodingOptions, filter, ref currentLineLength, ref scratchBuffer);
				mimeNode = mimeNode.NextSibling;
			}
			stream.Write(MimeString.Semicolon, 0, MimeString.Semicolon.Length);
			num += (long)MimeString.Semicolon.Length;
			currentLineLength.IncrementBy(MimeString.Semicolon.Length);
			return num;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000097E0 File Offset: 0x000079E0
		internal override MimeNode ParseNextChild()
		{
			MimeNode mimeNode = null;
			if (!this.parsed && base.Parent != null)
			{
				AddressHeader addressHeader = base.Parent as AddressHeader;
				if (addressHeader != null)
				{
					mimeNode = addressHeader.ParseNextMailBox(true);
				}
			}
			this.parsed = (mimeNode == null);
			return mimeNode;
		}

		// Token: 0x04000129 RID: 297
		private bool parsed;
	}
}
