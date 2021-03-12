using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000C0 RID: 192
	public class ContactReader : IDisposable
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x0002A0BA File Offset: 0x000282BA
		public ContactReader(Stream stream) : this(stream, Encoding.UTF8, ContactComplianceMode.Strict)
		{
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0002A0CC File Offset: 0x000282CC
		public ContactReader(Stream stream, Encoding encoding, ContactComplianceMode contactComplianceMode)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(CalendarStrings.StreamMustAllowRead, "stream");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this.reader = new ContentLineReader(stream, encoding, new ComplianceTracker(FormatType.VCard, (ComplianceMode)contactComplianceMode), new ContactValueTypeContainer());
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0002A12E File Offset: 0x0002832E
		public ContactComplianceMode ComplianceMode
		{
			get
			{
				this.CheckDisposed("ComplianceMode::get");
				return (ContactComplianceMode)this.reader.ComplianceTracker.Mode;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0002A14B File Offset: 0x0002834B
		public ContactComplianceStatus ComplianceStatus
		{
			get
			{
				this.CheckDisposed("ComplianceStatus::get");
				return (ContactComplianceStatus)this.reader.ComplianceTracker.Status;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0002A168 File Offset: 0x00028368
		public ContactPropertyReader PropertyReader
		{
			get
			{
				this.CheckDisposed("PropertyReader::get");
				this.reader.AssertValidState(~ContentLineNodeType.DocumentEnd);
				return new ContactPropertyReader(this.reader);
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0002A18D File Offset: 0x0002838D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0002A19C File Offset: 0x0002839C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isClosed && this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
			this.isClosed = true;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0002A1CA File Offset: 0x000283CA
		protected void CheckDisposed(string methodName)
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException("ContactReader", methodName);
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002A1E0 File Offset: 0x000283E0
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0002A1E8 File Offset: 0x000283E8
		public bool ReadNext()
		{
			this.CheckDisposed("ReadNext");
			if (this.reader.ReadNextComponent())
			{
				if (string.Compare(this.reader.ComponentName, "VCARD", StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.reader.ComplianceTracker.SetComplianceStatus(Microsoft.Exchange.Data.ContentTypes.Internal.ComplianceStatus.ComponentNameMismatch, CalendarStrings.ComponentNameMismatch);
				}
				if (this.reader.Depth > 1)
				{
					this.reader.ComplianceTracker.SetComplianceStatus(Microsoft.Exchange.Data.ContentTypes.Internal.ComplianceStatus.NotAllComponentsClosed, CalendarStrings.NotAllComponentsClosed);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0002A26A File Offset: 0x0002846A
		public void ResetComplianceStatus()
		{
			this.CheckDisposed("ResetComplianceStatus");
			this.reader.ComplianceTracker.Reset();
		}

		// Token: 0x0400066E RID: 1646
		private ContentLineReader reader;

		// Token: 0x0400066F RID: 1647
		private bool isClosed;
	}
}
