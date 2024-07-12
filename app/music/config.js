export const SERVER_DOMAIN = "http://lovemusic-dev.eba-b9p9cpex.ap-southeast-1.elasticbeanstalk.com/";

let albumId = null;

export const setAlbumId = (id) => {
  albumId = id;
};

export const getAlbumId = () => {
  return albumId;
};

let singerId = null;

export const setSingerId = (id) => {
    singerId = id;
};

export const getSingerId = () => {
  return singerId;
};