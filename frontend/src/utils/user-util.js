import StringUtil from '@/utils/string-util';

export const getFullname = (firstname, middlename, lastname) => {
  let fullname = firstname
  if (!StringUtil.isNullOrEmpty(middlename)) fullname += ` ${middlename}`
  fullname += ` ${lastname}`
  return fullname
}

export default {
  getFullname
}
