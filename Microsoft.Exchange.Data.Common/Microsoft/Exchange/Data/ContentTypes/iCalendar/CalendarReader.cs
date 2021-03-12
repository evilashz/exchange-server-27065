using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000B0 RID: 176
	public class CalendarReader : IDisposable
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00027B9F File Offset: 0x00025D9F
		public CalendarReader(Stream stream) : this(stream, "utf-8", CalendarComplianceMode.Strict)
		{
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00027BB0 File Offset: 0x00025DB0
		public CalendarReader(Stream stream, string encodingName, CalendarComplianceMode calendarComplianceMode)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(CalendarStrings.StreamMustAllowRead, "stream");
			}
			if (encodingName == null)
			{
				throw new ArgumentNullException("encodingName");
			}
			this.reader = new ContentLineReader(stream, Charset.GetEncoding(encodingName), new ComplianceTracker(FormatType.Calendar, (ComplianceMode)calendarComplianceMode), new CalendarValueTypeContainer());
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00027C17 File Offset: 0x00025E17
		public ComponentId ComponentId
		{
			get
			{
				this.CheckDisposed("ComponentId::get");
				return CalendarCommon.GetComponentEnum(this.ComponentName);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00027C2F File Offset: 0x00025E2F
		public string ComponentName
		{
			get
			{
				this.CheckDisposed("ComponentName::get");
				this.reader.AssertValidState(~ContentLineNodeType.DocumentEnd);
				return this.reader.ComponentName;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00027C54 File Offset: 0x00025E54
		public CalendarComplianceMode ComplianceMode
		{
			get
			{
				this.CheckDisposed("ComplianceMode::get");
				return (CalendarComplianceMode)this.reader.ComplianceTracker.Mode;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00027C71 File Offset: 0x00025E71
		public CalendarComplianceStatus ComplianceStatus
		{
			get
			{
				this.CheckDisposed("ComplianceStatus::get");
				return (CalendarComplianceStatus)this.reader.ComplianceTracker.Status;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00027C8E File Offset: 0x00025E8E
		public CalendarPropertyReader PropertyReader
		{
			get
			{
				this.CheckDisposed("PropertyReader::get");
				this.reader.AssertValidState(~ContentLineNodeType.DocumentEnd);
				return new CalendarPropertyReader(this.reader);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00027CB3 File Offset: 0x00025EB3
		public int Depth
		{
			get
			{
				this.CheckDisposed("Depth::get");
				return this.reader.Depth;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00027CCB File Offset: 0x00025ECB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00027CDA File Offset: 0x00025EDA
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isClosed && this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
			this.isClosed = true;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00027D08 File Offset: 0x00025F08
		protected void CheckDisposed(string methodName)
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException("CalendarReader", methodName);
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00027D1E File Offset: 0x00025F1E
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00027D26 File Offset: 0x00025F26
		public bool ReadNextComponent()
		{
			this.CheckDisposed("ReadNextComponent");
			return this.reader.ReadNextComponent();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00027D3E File Offset: 0x00025F3E
		public bool ReadFirstChildComponent()
		{
			this.CheckDisposed("ReadFirstChildComponent");
			this.reader.AssertValidState((ContentLineNodeType)(-1));
			return this.reader.ReadFirstChildComponent();
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00027D62 File Offset: 0x00025F62
		public bool ReadNextSiblingComponent()
		{
			this.CheckDisposed("ReadNextSiblingComponent");
			this.reader.AssertValidState((ContentLineNodeType)(-1));
			return this.reader.ReadNextSiblingComponent();
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00027D86 File Offset: 0x00025F86
		public void ResetComplianceStatus()
		{
			this.CheckDisposed("ResetComplianceStatus");
			this.reader.ComplianceTracker.Reset();
		}

		// Token: 0x040005D9 RID: 1497
		private ContentLineReader reader;

		// Token: 0x040005DA RID: 1498
		private bool isClosed;
	}
}
