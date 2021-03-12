using System;
using System.IO;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002BB RID: 699
	[DataContract]
	public class ImportContactListParameters : SetObjectProperties
	{
		// Token: 0x06002C0E RID: 11278 RVA: 0x00088C90 File Offset: 0x00086E90
		public ImportContactListParameters()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x00088CB2 File Offset: 0x00086EB2
		// (set) Token: 0x06002C10 RID: 11280 RVA: 0x00088CC4 File Offset: 0x00086EC4
		public Stream CSVStream
		{
			get
			{
				return (Stream)base["CSVStream"];
			}
			set
			{
				base["CSVStream"] = value;
			}
		}

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x00088CD2 File Offset: 0x00086ED2
		public override string AssociatedCmdlet
		{
			get
			{
				return "Import-ContactList";
			}
		}

		// Token: 0x17001DB4 RID: 7604
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x00088CD9 File Offset: 0x00086ED9
		public override string RbacScope
		{
			get
			{
				return "@W:Self|Organization";
			}
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x00088CE0 File Offset: 0x00086EE0
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["CSV"] = new SwitchParameter(true);
		}

		// Token: 0x040021D4 RID: 8660
		private const string CsvSuffix = "&CSV";

		// Token: 0x040021D5 RID: 8661
		private const string CsvStreamSuffix = "&CSVStream";

		// Token: 0x040021D6 RID: 8662
		public const string RbacParameters = "?Identity&CSV&CSVStream";
	}
}
