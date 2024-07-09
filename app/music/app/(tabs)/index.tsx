import React, { useState, useEffect } from 'react';
import { StyleSheet, View, Text, Image, ScrollView, TouchableOpacity } from 'react-native';

import AudioPlayer from '@/components/AudioPlayer';
import { SERVER_DOMAIN } from '../../config';

const App: React.FC = () => {
  const [musicData, setMusicData] = useState<MusicItem[]>([]); 

  useEffect(() => {
    const fetchMusicData = async () => {
      try {
        const response = await fetch(`${SERVER_DOMAIN}/api/Song/GetAll`);
        const json = await response.json();
        setMusicData(json);
      } catch (error) {
        console.error('Error fetching music data:', error);
      }
    };

    fetchMusicData();
  }, []);

  return (
    <ScrollView style={styles.container}>
      <Image
        source={require('../../assets/title.jpg')}
        style={styles.coverImage}
      />
      <View style={styles.coverImageTextContainer}>
        <Text style={styles.coverImageTitle}>BK nhạc</Text>
        <Text style={styles.coverImageSubtitle}>Hãy bắt đầu 1 ngày mới đầy năng lượng</Text>
      </View>

      {/* New Music */}
      <Text style={styles.title}>Bài hát mới</Text>
      <ScrollView horizontal={true} style={styles.horizontalScrollView}>
        {musicData.map((item) => (
          <View key={item._id} style={{ marginRight: 10 }}>
            <AudioPlayer
              url={item.audioUrl}
              img={item.imgUrl}
              name_music={item.name}
              name_singer={item.singerName}
              style={{ width: 100, height: 100, marginRight: 10 }}
            />
            <Text numberOfLines={1} style={styles.newMusicSinger}>{item.name}</Text>
            <Text numberOfLines={1} style={styles.newMusicName}>{item.singerName}</Text>
          </View>
        ))}
      </ScrollView>

      {/* Ablums */}
      <Text style={styles.title}>Ablums</Text>
      <ScrollView horizontal={true} style={styles.horizontalScrollView}>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerAlbumHot/edm.jpg')} 
            style={styles.albumsImg}
          />
          <Text numberOfLines={1} style={styles.albumsTitle}>Đỉnh Cao EDM</Text>
        </View>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerAlbumHot/nhac-han.jpg')} 
            style={styles.albumsImg}
          />
          <Text numberOfLines={1} style={styles.albumsTitle}>Những Bài Hát Hay Nhất HÀN "XẺNG"</Text>
        </View>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerAlbumHot/nhac-tre.jpg')} 
            style={styles.albumsImg}
          />
          <Text numberOfLines={1} style={styles.albumsTitle}>Nhạc Trẻ Gây Nghiện</Text>
        </View>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerAlbumHot/rap-viet.jpg')} 
            style={styles.albumsImg} 
          />
          <Text numberOfLines={1} style={styles.albumsTitle}>Cháy Hết Mình Với Những Bản Rap Hay Nhất Mọi Thời Đại</Text>
        </View>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerAlbumHot/au-my.jpg')} 
            style={styles.albumsImg} 
          />
          <Text numberOfLines={1} style={styles.albumsTitle}>Đỉnh Cao Nhạc Pop, Nghe Như Không Nghe !!!</Text>
        </View>
      </ScrollView>

      {/* Nghệ sỹ */}
      <Text style={styles.title}>Nghệ sỹ</Text>
      <ScrollView horizontal={true} style={styles.horizontalScrollView}>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerSinger/son-tung-mtp.jpg')} 
            style={styles.singersImg}
          />
          <Text numberOfLines={1} style={styles.singersTitle}>Đỉnh Cao EDM</Text>
        </View>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerSinger/ho-quang-hieu.jpg')}  
            style={styles.singersImg}
          />
          <Text numberOfLines={1} style={styles.singersTitle}>Hồ Quang Hiếu</Text>
        </View>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerSinger/jack-97.jpg')} 
            style={styles.singersImg}
          />
          <Text numberOfLines={1} style={styles.singersTitle}>Jack 97</Text>
        </View>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerSinger/phan-manh-quynh.jpg')} 
            style={styles.singersImg} 
          />
          <Text numberOfLines={1} style={styles.singersTitle}>Phan Mạnh Quỳnh</Text>
        </View>
        <View style={{ marginRight: 20 }}>
          <Image 
            source={require('../../assets/BannerSinger/g5-squad.jpg')} 
            style={styles.singersImg} 
          />
          <Text numberOfLines={1} style={styles.singersTitle}>G5 Squad</Text>
        </View>
      </ScrollView>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  coverImage: {
    width: '100%',
    height: 180,
  },
  coverImageTextContainer: {
    position: 'absolute',
    top: 100,
  },
  coverImageTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    left: 20,
  },
  title: {
    fontSize: 24,
    height: 50,
    fontWeight: 'bold',
    left: 20,
    top: 10,
  },
  coverImageSubtitle: {
    fontSize: 16,
    left: 20,
  },
  horizontalScrollView: {
    paddingHorizontal: 10,
  },
  newMusicSinger: {
    width: 100,
    fontWeight: 'bold',
  },
  newMusicName: {
    width: 100,
  },

  // Ablbums css
  albumsImg: {
    width: 100,
    height: 100,
    borderRadius: 50
  },
  albumsTitle: {
    width: 100,
    fontWeight: 'bold',
  },

  // Singer css
  singersImg: {
    width: 100,
    height: 100,
  },
  singersTitle: {
    width: 100,
    fontWeight: 'bold',
    marginBottom: 20
  },
});

interface MusicItem {
  _id: string;
  audioUrl: string;
  imgUrl: string;
  name: string;
  singerName: string;
  view: string;
  likes: string;
}

export default App;
