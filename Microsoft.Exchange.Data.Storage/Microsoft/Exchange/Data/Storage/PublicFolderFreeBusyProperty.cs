using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CAD RID: 3245
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class PublicFolderFreeBusyProperty : SmartPropertyDefinition
	{
		// Token: 0x0600711C RID: 28956 RVA: 0x001F575F File Offset: 0x001F395F
		public PublicFolderFreeBusyProperty() : base("PublicFolderFreeBusy", typeof(PublicFolderFreeBusy), PropertyFlags.None, PropertyDefinitionConstraint.None, PublicFolderFreeBusyProperty.dependantProps)
		{
		}

		// Token: 0x0600711D RID: 28957 RVA: 0x001F5784 File Offset: 0x001F3984
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			ExDateTime exDateTime = ExDateTime.MaxValue;
			ExDateTime t = ExDateTime.MinValue;
			List<PublicFolderFreeBusyAppointment> list = new List<PublicFolderFreeBusyAppointment>();
			foreach (PublicFolderFreeBusyProperty.FreeBusyPropertySet freeBusyPropertySet in PublicFolderFreeBusyProperty.FreeBusyPropertySets)
			{
				int[] array = propertyBag.GetValue(freeBusyPropertySet.PublishMonths) as int[];
				if (array != null && array.Length != 0)
				{
					byte[][] array2 = propertyBag.GetValue(freeBusyPropertySet.Appointments) as byte[][];
					object result;
					if (array2 == null || array2.Length == 0)
					{
						PublicFolderFreeBusyProperty.Tracer.TraceError((long)this.GetHashCode(), "PublicFolderFreeBusyProperty::InternalTryGetValue. No data.");
						result = PublicFolderFreeBusyProperty.calculatedPropertyError;
					}
					else
					{
						if (array2.Length == array.Length)
						{
							ExDateTime[] array3 = new ExDateTime[array.Length];
							for (int j = 0; j < array.Length; j++)
							{
								array3[j] = PublicFolderFreeBusyProperty.PublishMonthPropertyConverter.FromInt(array[j]);
							}
							foreach (ExDateTime exDateTime2 in array3)
							{
								if (exDateTime2 < exDateTime)
								{
									exDateTime = exDateTime2;
								}
								if (exDateTime2 > t)
								{
									t = exDateTime2;
								}
							}
							IEnumerable<PublicFolderFreeBusyAppointment> collection = PublicFolderFreeBusyProperty.AppointmentsPropertyConverter.FromBinary(array2, array3, freeBusyPropertySet.BusyType);
							list.AddRange(collection);
							goto IL_16A;
						}
						PublicFolderFreeBusyProperty.Tracer.TraceError<int, int>((long)this.GetHashCode(), "PublicFolderFreeBusyProperty::InternalTryGetValue. Appointments array length {0} does not match the publish month length {1}.", array2.Length, array.Length);
						result = PublicFolderFreeBusyProperty.calculatedPropertyError;
					}
					return result;
				}
				PublicFolderFreeBusyProperty.Tracer.TraceError<BusyType>((long)this.GetHashCode(), "PublicFolderFreeBusyProperty::InternalTryGetValue. Unable to retrieve the publish months information for Property Set {0}.", freeBusyPropertySet.BusyType);
				IL_16A:;
			}
			int numberOfMonths = exDateTime.Year * 12 + exDateTime.Month - (exDateTime.Year * 12 + exDateTime.Month) + 1;
			return new PublicFolderFreeBusy
			{
				StartDate = exDateTime,
				NumberOfMonths = numberOfMonths,
				Appointments = list
			};
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x001F5964 File Offset: 0x001F3B64
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			PublicFolderFreeBusy publicFolderFreeBusy = value as PublicFolderFreeBusy;
			if (publicFolderFreeBusy == null)
			{
				throw new ArgumentException("value");
			}
			IEnumerable<PublicFolderFreeBusyAppointment> sortedAppointments = from appointment in publicFolderFreeBusy.Appointments
			orderby appointment.StartTime
			select appointment;
			ExDateTime startDate = publicFolderFreeBusy.StartDate;
			int numberOfMonths = publicFolderFreeBusy.NumberOfMonths;
			int[] array = new int[numberOfMonths];
			for (int i = 0; i < numberOfMonths; i++)
			{
				ExDateTime startMonth = startDate.AddMonths(i);
				array[i] = PublicFolderFreeBusyProperty.PublishMonthPropertyConverter.ToInt(startMonth);
			}
			foreach (PublicFolderFreeBusyProperty.FreeBusyPropertySet freeBusyPropertySet in PublicFolderFreeBusyProperty.FreeBusyPropertySets)
			{
				byte[][] propertyValue = PublicFolderFreeBusyProperty.AppointmentsPropertyConverter.ToBinary(sortedAppointments, freeBusyPropertySet.BusyType, startDate, numberOfMonths);
				propertyBag.SetValue(freeBusyPropertySet.Appointments, propertyValue);
				propertyBag.SetValue(freeBusyPropertySet.PublishMonths, array);
			}
		}

		// Token: 0x04004E98 RID: 20120
		private static readonly Trace Tracer = ExTraceGlobals.StorageTracer;

		// Token: 0x04004E99 RID: 20121
		private static readonly PropertyError calculatedPropertyError = new PropertyError(InternalSchema.PublicFolderFreeBusy, PropertyErrorCode.GetCalculatedPropertyError);

		// Token: 0x04004E9A RID: 20122
		private static readonly PropertyDependency[] dependantProps = new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ScheduleInfoFreeBusyBusy, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ScheduleInfoMonthsBusy, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ScheduleInfoFreeBusyTentative, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ScheduleInfoMonthsTentative, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ScheduleInfoFreeBusyOof, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ScheduleInfoMonthsOof, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ScheduleInfoFreeBusyMerged, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ScheduleInfoMonthsMerged, PropertyDependencyType.AllRead)
		};

		// Token: 0x04004E9B RID: 20123
		private static readonly PublicFolderFreeBusyProperty.FreeBusyPropertySet[] FreeBusyPropertySets = new PublicFolderFreeBusyProperty.FreeBusyPropertySet[]
		{
			new PublicFolderFreeBusyProperty.FreeBusyPropertySet
			{
				Appointments = InternalSchema.ScheduleInfoFreeBusyBusy,
				PublishMonths = InternalSchema.ScheduleInfoMonthsBusy,
				BusyType = BusyType.Busy
			},
			new PublicFolderFreeBusyProperty.FreeBusyPropertySet
			{
				Appointments = InternalSchema.ScheduleInfoFreeBusyTentative,
				PublishMonths = InternalSchema.ScheduleInfoMonthsTentative,
				BusyType = BusyType.Tentative
			},
			new PublicFolderFreeBusyProperty.FreeBusyPropertySet
			{
				Appointments = InternalSchema.ScheduleInfoFreeBusyOof,
				PublishMonths = InternalSchema.ScheduleInfoMonthsOof,
				BusyType = BusyType.OOF
			},
			new PublicFolderFreeBusyProperty.FreeBusyPropertySet
			{
				Appointments = InternalSchema.ScheduleInfoFreeBusyMerged,
				PublishMonths = InternalSchema.ScheduleInfoMonthsMerged,
				BusyType = BusyType.Unknown
			}
		};

		// Token: 0x02000CAE RID: 3246
		private static class AppointmentsPropertyConverter
		{
			// Token: 0x06007121 RID: 28961 RVA: 0x001F5BA0 File Offset: 0x001F3DA0
			public static byte[][] ToBinary(IEnumerable<PublicFolderFreeBusyAppointment> sortedAppointments, BusyType requestedFreeBusyType, ExDateTime startMonth, int numberOfMonths)
			{
				byte[][] array = new byte[numberOfMonths][];
				ExDateTime exDateTime = startMonth.AddMonths(1);
				bool flag = true;
				for (int i = 0; i < array.Length; i++)
				{
					double totalMinutes = (exDateTime - startMonth).TotalMinutes;
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
						{
							ushort num = 0;
							foreach (PublicFolderFreeBusyAppointment publicFolderFreeBusyAppointment in sortedAppointments)
							{
								if (!(publicFolderFreeBusyAppointment.EndTime < startMonth))
								{
									if (publicFolderFreeBusyAppointment.StartTime >= exDateTime)
									{
										break;
									}
									bool flag2 = publicFolderFreeBusyAppointment.BusyType == requestedFreeBusyType || (requestedFreeBusyType == BusyType.Unknown && publicFolderFreeBusyAppointment.BusyType != BusyType.Free && publicFolderFreeBusyAppointment.BusyType != BusyType.WorkingElseWhere);
									if (flag2)
									{
										double totalMinutes2 = (publicFolderFreeBusyAppointment.StartTime - startMonth).TotalMinutes;
										ushort num2;
										if (totalMinutes2 < 0.0)
										{
											num2 = 0;
										}
										else
										{
											num2 = (ushort)totalMinutes2;
										}
										double totalMinutes3 = (publicFolderFreeBusyAppointment.EndTime - startMonth).TotalMinutes;
										ushort val;
										if (totalMinutes3 > totalMinutes)
										{
											val = (ushort)totalMinutes;
										}
										else
										{
											val = (ushort)totalMinutes3;
										}
										if (flag)
										{
											binaryWriter.Write(num2);
											flag = false;
										}
										else if (num2 > num)
										{
											binaryWriter.Write(num);
											binaryWriter.Write(num2);
										}
										num = Math.Max(num, val);
									}
								}
							}
							if (num != 0)
							{
								binaryWriter.Write(num);
							}
						}
						array[i] = memoryStream.ToArray();
						startMonth = exDateTime;
						exDateTime = startMonth.AddMonths(1);
						flag = true;
					}
				}
				return array;
			}

			// Token: 0x06007122 RID: 28962 RVA: 0x001F5D9C File Offset: 0x001F3F9C
			public static IEnumerable<PublicFolderFreeBusyAppointment> FromBinary(byte[][] binaryData, ExDateTime[] publishMonths, BusyType busyType)
			{
				List<PublicFolderFreeBusyAppointment> list = new List<PublicFolderFreeBusyAppointment>();
				for (int i = 0; i < binaryData.Length; i++)
				{
					ExDateTime exDateTime = publishMonths[i];
					using (MemoryStream memoryStream = new MemoryStream(binaryData[i]))
					{
						using (BinaryReader binaryReader = new BinaryReader(memoryStream))
						{
							while (memoryStream.Position + 4L <= memoryStream.Length)
							{
								ushort num = binaryReader.ReadUInt16();
								ushort num2 = binaryReader.ReadUInt16();
								list.Add(new PublicFolderFreeBusyAppointment(exDateTime.AddMinutes((double)num), exDateTime.AddMinutes((double)num2), busyType));
							}
						}
					}
				}
				return list;
			}
		}

		// Token: 0x02000CAF RID: 3247
		private static class PublishMonthPropertyConverter
		{
			// Token: 0x06007123 RID: 28963 RVA: 0x001F5E60 File Offset: 0x001F4060
			public static int ToInt(ExDateTime startMonth)
			{
				return startMonth.Year * 16 + startMonth.Month;
			}

			// Token: 0x06007124 RID: 28964 RVA: 0x001F5E74 File Offset: 0x001F4074
			public static ExDateTime FromInt(int publishMonth)
			{
				int year = publishMonth >> 4;
				int month = publishMonth & 15;
				return new ExDateTime(ExTimeZone.UtcTimeZone, year, month, 1);
			}
		}

		// Token: 0x02000CB0 RID: 3248
		private sealed class FreeBusyPropertySet
		{
			// Token: 0x17001E4F RID: 7759
			// (get) Token: 0x06007125 RID: 28965 RVA: 0x001F5E97 File Offset: 0x001F4097
			// (set) Token: 0x06007126 RID: 28966 RVA: 0x001F5E9F File Offset: 0x001F409F
			public PropertyTagPropertyDefinition Appointments { get; set; }

			// Token: 0x17001E50 RID: 7760
			// (get) Token: 0x06007127 RID: 28967 RVA: 0x001F5EA8 File Offset: 0x001F40A8
			// (set) Token: 0x06007128 RID: 28968 RVA: 0x001F5EB0 File Offset: 0x001F40B0
			public PropertyTagPropertyDefinition PublishMonths { get; set; }

			// Token: 0x17001E51 RID: 7761
			// (get) Token: 0x06007129 RID: 28969 RVA: 0x001F5EB9 File Offset: 0x001F40B9
			// (set) Token: 0x0600712A RID: 28970 RVA: 0x001F5EC1 File Offset: 0x001F40C1
			public BusyType BusyType { get; set; }
		}
	}
}
