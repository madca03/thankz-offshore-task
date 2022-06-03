import HttpUtil from '@/utils/http-util'
import API from '@/references/api-endpoints'
import HttpMethods from 'http-methods-constants';
import MimeTypes from '@/references/mime-types'
import StringUtil from '@/utils/string-util'

export default {
  getUsers({limit = 0, offset = 0, csv = false} = {}) {
    return HttpUtil.instance.makeHTTPRequest({
      url: API.GET_USERS,
      payload: {
        limit,
        offset,
        csv
      }
    }).then(res => res.users)
  },
  getUserById(userId) {
    return HttpUtil.instance.makeHTTPRequest({
      url: API.GET_USER_BY_ID(userId)
    }).then(res => res.user)
  },
  createUser(user) {
    return HttpUtil.instance.makeHTTPRequest({
      url: API.CREATE_USER,
      method: HttpMethods.POST,
      payload: user
    }).then(res => res.user)
  },
  createUserFromFile(file) {
    let fileType = MimeTypes.MimeTypeToFileType[file.type]
    if (StringUtil.isNullOrEmpty(fileType)) return Promise.reject()

    const payload = new FormData()
    payload.append('userfile', file)
    payload.append('filetype', fileType)

    return HttpUtil.instance.makeHTTPRequest({
      url: API.CREATE_USERS_FROM_FILE,
      method: HttpMethods.POST,
      payload
    }).then(res => res.users)
  },
  updateUser(user) {
    const userId = user.userId
    delete user.userId

    return HttpUtil.instance.makeHTTPRequest({
      url: API.UPDATE_USER(userId),
      method: HttpMethods.PUT,
      payload: user
    }).then(res => res.user)
  },
  deleteUser(userId) {
    return HttpUtil.instance.makeHTTPRequest({
      url: API.DELETE_USER(userId),
      method: HttpMethods.DELETE
    }).then(res => res.user)
  }
}
