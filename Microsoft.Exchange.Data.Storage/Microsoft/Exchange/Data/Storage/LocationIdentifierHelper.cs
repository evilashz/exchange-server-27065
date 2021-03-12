using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000376 RID: 886
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LocationIdentifierHelper : ILocationIdentifierSetter, IEnumerable<LocationIdentifier>, IEnumerable
	{
		// Token: 0x06002716 RID: 10006 RVA: 0x0009C930 File Offset: 0x0009AB30
		public LocationIdentifierHelper()
		{
			this.PrivateResetChangeList();
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x0009C93E File Offset: 0x0009AB3E
		public static byte[] LocationIdentifierBufferIdentifier
		{
			get
			{
				if (LocationIdentifierHelper.locationIdentifierBufferIdentifier == null)
				{
					LocationIdentifierHelper.locationIdentifierBufferIdentifier = CTSGlobals.AsciiEncoding.GetBytes("v2CalendarLogging");
				}
				return LocationIdentifierHelper.locationIdentifierBufferIdentifier;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x0009C960 File Offset: 0x0009AB60
		public static int LocationIdentifierBufferIdentifierSize
		{
			get
			{
				return LocationIdentifierHelper.LocationIdentifierBufferIdentifier.Length;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x0009C969 File Offset: 0x0009AB69
		public long ChangeBufferSize
		{
			get
			{
				if (this.changeList != null)
				{
					return (long)LocationIdentifierHelper.LocationIdentifierBufferIdentifierSize + (long)LocationIdentifier.ByteArraySize * (long)this.changeList.Count;
				}
				return 0L;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x0009C990 File Offset: 0x0009AB90
		// (set) Token: 0x0600271B RID: 10011 RVA: 0x0009CA24 File Offset: 0x0009AC24
		public byte[] ChangeBuffer
		{
			get
			{
				if (this.changeList == null)
				{
					return null;
				}
				byte[] array = new byte[this.ChangeBufferSize];
				Array.Copy(LocationIdentifierHelper.LocationIdentifierBufferIdentifier, array, LocationIdentifierHelper.LocationIdentifierBufferIdentifierSize);
				int num = LocationIdentifierHelper.LocationIdentifierBufferIdentifierSize;
				foreach (LocationIdentifier locationIdentifier in this.changeList)
				{
					byte[] byteArray = locationIdentifier.ByteArray;
					Array.Copy(byteArray, 0, array, num, byteArray.Length);
					num += byteArray.Length;
				}
				return array;
			}
			set
			{
				if (value == null)
				{
					this.changeList = null;
					return;
				}
				int i = LocationIdentifierHelper.LocationIdentifierBufferIdentifierSize;
				int num = (value.Length - i) / LocationIdentifier.ByteArraySize;
				int capacity = (num > int.MaxValue) ? int.MaxValue : num;
				this.changeList = new List<LocationIdentifier>(capacity);
				while (i < value.Length)
				{
					byte[] array = new byte[LocationIdentifier.ByteArraySize];
					Array.Copy(value, i, array, 0, array.Length);
					LocationIdentifier item = new LocationIdentifier(array);
					this.changeList.Add(item);
					i += array.Length;
				}
			}
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0009CAA8 File Offset: 0x0009ACA8
		public string ChangeListToString()
		{
			if (this.changeList == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (LocationIdentifier arg in this.changeList)
			{
				stringBuilder.AppendFormat("{0};", arg);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x0009CB18 File Offset: 0x0009AD18
		public void ParseChangeList(string str)
		{
			if (str == null)
			{
				this.changeList = null;
				return;
			}
			if (str.Length != 0)
			{
				string[] array = str.Split(new char[]
				{
					';'
				});
				if (this.changeList == null)
				{
					this.changeList = new List<LocationIdentifier>(array.Length);
				}
				else
				{
					this.changeList.Clear();
					if (this.changeList.Capacity < array.Length)
					{
						this.changeList.Capacity = array.Length;
					}
				}
				foreach (string text in array)
				{
					if (text.Length == 0)
					{
						return;
					}
					LocationIdentifier item;
					try
					{
						item = LocationIdentifier.Parse(text);
					}
					catch (ArgumentException innerException)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a valid Location Identifier representaion.", new object[]
						{
							text
						}), innerException);
					}
					this.changeList.Add(item);
				}
				return;
			}
			if (this.changeList == null)
			{
				this.changeList = new List<LocationIdentifier>(0);
				return;
			}
			this.changeList.Clear();
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0009CC24 File Offset: 0x0009AE24
		public void SetLocationIdentifier(uint id)
		{
			LocationIdentifier locationIdentifier = new LocationIdentifier(id);
			this.SetLocationIdentifier(locationIdentifier);
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0009CC40 File Offset: 0x0009AE40
		public void SetLocationIdentifier(uint id, LastChangeAction action)
		{
			EnumValidator.ThrowIfInvalid<LastChangeAction>(action);
			LocationIdentifier locationIdentifier = new LocationIdentifier(id, action);
			this.SetLocationIdentifier(locationIdentifier);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0009CC62 File Offset: 0x0009AE62
		IEnumerator<LocationIdentifier> IEnumerable<LocationIdentifier>.GetEnumerator()
		{
			return ((IEnumerable<LocationIdentifier>)this.changeList).GetEnumerator();
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0009CC6F File Offset: 0x0009AE6F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.changeList).GetEnumerator();
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0009CC7C File Offset: 0x0009AE7C
		internal virtual void ResetChangeList()
		{
			this.PrivateResetChangeList();
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0009CC84 File Offset: 0x0009AE84
		protected virtual void SetLocationIdentifier(LocationIdentifier locationIdentifier)
		{
			if (this.changeList == null)
			{
				this.ResetChangeList();
			}
			this.changeList.Add(locationIdentifier);
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0009CCA0 File Offset: 0x0009AEA0
		private void PrivateResetChangeList()
		{
			if (this.changeList == null)
			{
				this.changeList = new List<LocationIdentifier>(1);
				return;
			}
			this.changeList.Clear();
		}

		// Token: 0x04001732 RID: 5938
		private const string LocationIdentifierBufferHeader = "v2CalendarLogging";

		// Token: 0x04001733 RID: 5939
		private static byte[] locationIdentifierBufferIdentifier;

		// Token: 0x04001734 RID: 5940
		private List<LocationIdentifier> changeList;
	}
}
