using Common.Dto.Report.Report;
using System.Collections;
using System.Collections.Generic;
using Common.Dto.Report.Enums;
using System;

namespace Report.Test
{
    public class TestDataHelper
    {
        public class InsertReportClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new InsertReportRequestDto()
                    {
                        ReportTypeId = EnumReportTypeDto.ContactReport,
                        ServiceTypeId = EnumServiceTypeDto.ContactService
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class UpdateReportClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new UpdateReportRequestDto()
                    {
                        Id = GetTestReportId(),
                        CompletedDate = DateTime.Now,
                        RequestDate = DateTime.Now.AddDays(-1),
                        Status = EnumReportStatusTypeDto.Completed,
                        ReportTypeId = EnumReportTypeDto.ContactReport,
                        ServiceTypeId = EnumServiceTypeDto.ContactService,
                        FilePath = Guid.NewGuid().ToString()
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public static string GetTestReportId()
        {
            return "507f1f77bcf86cd799439011";
        }

        public static string GetTestReportTypeId()
        {
            return "507f191e810c19729de860ea";
        }
    }
}
