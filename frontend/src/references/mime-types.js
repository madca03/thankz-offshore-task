export const xlsx = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
export const csv = 'text/csv'
export const MimeTypeToFileType = {
  [xlsx]: 'xlsx',
  [csv]: 'csv'
}

export default {
  xlsx,
  csv,
  MimeTypeToFileType
}
